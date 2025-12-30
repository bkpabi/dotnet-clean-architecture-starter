using CleanArch.Domain.SeedWork;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using ValidationException = CleanArch.ApplicationCore.ApplicationExceptions.ValidationException;


namespace CleanArch.ApplicationCore.Behaviours;

public class UnitOfWorkBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public UnitOfWorkBehaviour(IUnitOfWork unitOfWork, IEnumerable<IValidator<TRequest>> validators)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        //Check if there is any validators defined for this command
        if (_validators.Any())
        {
            // If yes check if there is any validation errors
            var context = new ValidationContext<TRequest>(request);
            var errorsDictionary = _validators
                .Select(x => x.Validate(context))
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .GroupBy(
                    x => x.PropertyName,
                    x => x.ErrorMessage,
                    (propertyName, errorMessages) => new
                    {
                        Key = propertyName,
                        Values = errorMessages.Distinct().ToArray()
                    })
                .ToDictionary(x => x.Key, x => x.Values);

            if (errorsDictionary.Any())
            {
                throw new ValidationException(errorsDictionary);
            }
        }

        if (!typeof(TRequest).Name.EndsWith("Command"))
        {
            return await next();
        }
        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var response = await next();
            try
            {
                var isSuccess = await _unitOfWork.SaveChangesAsync(cancellationToken);
                scope.Complete();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }
    }
}
