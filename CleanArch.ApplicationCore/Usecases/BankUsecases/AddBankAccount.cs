using AutoMapper;
using CleanArch.ApplicationCore.Contracts;
using CleanArch.ApplicationCore.DTOs;
using CleanArch.ApplicationCore.Responses;
using CleanArch.Domain.Entities.BankAggregate;
using CleanArch.Domain.SeedWork;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.ApplicationCore.Usecases.BankUsecases;

public record AddBankAccountCommand(BankAccountDTO bankAccount) : IRequest<HandlerResponse>;

public class AddBankAccountCommandValidator : AbstractValidator<AddBankAccountCommand>
{
    public AddBankAccountCommandValidator()
    {
        RuleFor(e => e.bankAccount.AccountName).NotEmpty().WithMessage("{PropertyName} is required");
        RuleFor(e => e.bankAccount.AccountNumber).NotEmpty().WithMessage("{PropertyName} is required");
        RuleFor(e => e.bankAccount.AccountTypeId).NotEmpty().WithMessage("{PropertyName} is required");
        RuleFor(e => e.bankAccount.InstitutionId).NotEmpty().WithMessage("{PropertyName} is required");
        RuleFor(e => e.bankAccount.CurrentBalance).GreaterThanOrEqualTo(0).WithMessage("{PropertyName} can not be a negetive value");
    }
}


public class AddBankAccountCommandHandler : IRequestHandler<AddBankAccountCommand, HandlerResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthenticatedUser _authenticatedUser;
    private readonly IMapper _mapper;

    public AddBankAccountCommandHandler(IUnitOfWork unitOfWork, IAuthenticatedUser authenticatedUser, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _authenticatedUser = authenticatedUser;
        _mapper = mapper;
    }

    public async Task<HandlerResponse> Handle(AddBankAccountCommand request, CancellationToken cancellationToken)
    {
        HandlerResponse handlerResponse = new HandlerResponse();

        // Check for duplication
        var bankAccountList = _unitOfWork.BankAccountRepository.GetBankAccountsByUser(request.bankAccount.UserId, cancellationToken);
        var isDuplicate = bankAccountList.Where(e => e.IsActive == true).Any(e => e.AccountName == request.bankAccount.AccountName || e.AccountNumber == request.bankAccount.AccountNumber);

        if (isDuplicate)
        {
            handlerResponse.Success = false;
            handlerResponse.Errors.Add("Bank account with similar details already present.");
            return handlerResponse;
        }

        request.bankAccount.CreatedBy = _authenticatedUser.UserId;
        request.bankAccount.CreatedOn = DateTime.UtcNow;
        request.bankAccount.UserId = _authenticatedUser.UserId;

        handlerResponse.Success = await _unitOfWork.BankAccountRepository.Add(_mapper.Map<BankAccountDTO, BankAccount>(request.bankAccount));
        handlerResponse.Message = "Bank detail saved";
        return handlerResponse;
    }
}