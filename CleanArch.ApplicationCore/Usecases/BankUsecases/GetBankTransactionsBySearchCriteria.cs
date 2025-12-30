using CleanArch.ApplicationCore.Contracts;
using CleanArch.ApplicationCore.DTOs;
using Dapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.ApplicationCore.Usecases.BankUsecases;

public record GetTransactionsBySearchCriteriaQuery(BankTransactionSearchCriteriaDTO searchCriteria) : IRequest<IEnumerable<BankTransactionDTO>>;

public class GetTransactionsBySearchCriteriaQueryHandler : IRequestHandler<GetTransactionsBySearchCriteriaQuery, IEnumerable<BankTransactionDTO>>
{
    private readonly IDbConnection _dbConnection;
    private readonly IAuthenticatedUser _authenticatedUser;

    public GetTransactionsBySearchCriteriaQueryHandler(IDbConnection connection, IAuthenticatedUser authenticatedUser)
    {
        _dbConnection = connection;
        _authenticatedUser = authenticatedUser;
    }
    public async Task<IEnumerable<BankTransactionDTO>> Handle(GetTransactionsBySearchCriteriaQuery request, CancellationToken cancellationToken)
    {
        var sql = new StringBuilder();
        sql.Append("select BT.*, C.CategoryName as Category, T.Name as TransactionType, BA.AccountName as BankName ");
        sql.Append("from BankTransactions as BT ");
        sql.Append("join BankAccounts as BA on BT.BankAccountId = BA.Id ");
        sql.Append("join Categories as C on BT.CategoryId = C.Id ");
        sql.Append("join TransactionTypes as T on BT.TransactionTypeId = T.Id ");
        sql.Append("join CategoryTypes as CT on CT.Id = C.CategoryTypeId ");
        sql.Append("where 1=1 and BA.UserId = @UserId ");

        var parameters = new DynamicParameters();
        parameters.Add("@UserId", _authenticatedUser.UserId);
        

        if (request.searchCriteria.BankAccountId != Guid.Empty)
        {
            sql.Append("and BT.BankAccountId = @BankAccountId ");
            parameters.Add("@BankAccountId", request.searchCriteria.BankAccountId);
        }

        if (request.searchCriteria.StartDate > DateTime.MinValue)
        {
            sql.Append("and CAST(BT.TransactionDate AS DATE) >= @StartDate ");
            parameters.Add("@StartDate", request.searchCriteria.StartDate);
        }

        if (request.searchCriteria.EndDate > DateTime.MinValue)
        {
            sql.Append("and CAST(BT.TransactionDate AS DATE) <= @EndDate ");
            parameters.Add("@EndDate", request.searchCriteria.EndDate);
        }
        
        if (!string.IsNullOrEmpty(request.searchCriteria.ReferenceNumber))
        {
            sql.Append("and BT.TransactionReference = @ReferenceNumber ");
            parameters.Add("@ReferenceNumber", request.searchCriteria.ReferenceNumber);
        }

        if (request.searchCriteria.CategoryList != null && request.searchCriteria.CategoryList.Any())
        {
            var inClause = new StringBuilder();
            for (int i = 0; i < request.searchCriteria.CategoryList.Count; i++)
            {
                var paramName = $"@CategoryList{i}";
                inClause.Append(paramName);
                if (i < request.searchCriteria.CategoryList.Count - 1)
                    inClause.Append(", ");
                parameters.Add(paramName, request.searchCriteria.CategoryList[i]);
            }
            sql.Append($"and C.Id IN ({inClause}) ");
        }
        else
        {
            if (!string.IsNullOrEmpty(request.searchCriteria.Category))
            {
                sql.Append("and C.CategoryName = @Category ");
                parameters.Add("@Category", request.searchCriteria.Category);
            }
        }

        if (request.searchCriteria.CategoryId != Guid.Empty)
        {
            sql.Append("and BT.CategoryId = @CategoryId ");
            parameters.Add("@CategoryId", request.searchCriteria.CategoryId);
        }
        if (!string.IsNullOrEmpty(request.searchCriteria.CategoryType))
        {
            sql.Append("and CT.CategoryTypeName = @CategoryType ");
            parameters.Add("@CategoryType", request.searchCriteria.CategoryType);
        }
        if (!string.IsNullOrEmpty(request.searchCriteria.TransactionType))
        {
            sql.Append("and T.Name = @TransactionType ");
            parameters.Add("@TransactionType", request.searchCriteria.TransactionType);
        }
        if (request.searchCriteria.Amount > 0)
        {
            sql.Append("and BT.TransactionAmount = @Amount ");
            parameters.Add("@Amount", request.searchCriteria.Amount);
        }
        if (!string.IsNullOrEmpty(request.searchCriteria.Description))
        {
            sql.Append("and BT.TransactionDescription like @Description ");
            parameters.Add("@Description", $"%{request.searchCriteria.Description}%");
        }

        var command = new CommandDefinition(sql.ToString(), parameters, cancellationToken: cancellationToken);
        var transactions = await _dbConnection.QueryAsync<BankTransactionDTO>(command);
        return transactions;
    }
}