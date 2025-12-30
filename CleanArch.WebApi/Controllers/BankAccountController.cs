using CleanArch.ApplicationCore.DTOs;
using CleanArch.ApplicationCore.Responses;
using CleanArch.ApplicationCore.Usecases.BankUsecases;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BankAccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost, Route("AddBankAccount")]
        public async Task<ActionResult<HandlerResponse>> AddBankAccount(BankAccountDTO bankAccount)
        {
            var command = new AddBankAccountCommand(bankAccount);
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet, Route("GetBankTransactions")]
        public async Task<ActionResult<List<BankTransactionDTO>>> GetBankTransactionsBySearchCriteria(BankTransactionSearchCriteriaDTO searchCriteriaDTO)
        {
            var query = new GetTransactionsBySearchCriteriaQuery(searchCriteriaDTO);
            var response = await _mediator.Send(query);
            return Ok(response.ToList());
        }
    }
}
