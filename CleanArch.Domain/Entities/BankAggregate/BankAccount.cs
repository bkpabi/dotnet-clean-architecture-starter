using CleanArch.Domain.SeedWork;
using CleanArch.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Domain.Entities.BankAggregate;

public class BankAccount : BaseEntity, IAggregateRoot
{
    public BankAccount()
    {
    }

    public BankAccount(string accountName
        , string accountNumber
        , Guid institutionId
        , Guid accountTypeId
        , decimal currentBalance
        , string userId
        , bool isActive
        , bool autoSync
        , string userName
        , string password
        , string createdBy
        , string? updatedBy
        , DateTime createOn
        , DateTime? updateOn
        , List<BankTransaction> bankTransactions)
    {
        AccountName = accountName;
        AccountNumber = accountNumber;
        InstitutionId = institutionId;
        AccountTypeId = accountTypeId;
        CurrentBalance = currentBalance;
        UserId = userId;
        IsActive = isActive;
        CreatedBy = createdBy;
        UpdatedBy = updatedBy;
        CreatedOn = createOn;
        UpdatedOn = updateOn;
        _transactions = bankTransactions;
    }
    public string AccountName { get; private set; } = string.Empty;
    public string? AccountNumber { get; private set; } = string.Empty;
    public Guid InstitutionId { get; private set; }
    public Guid AccountTypeId { get; private set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal CurrentBalance { get; private set; }
    public string UserId { get; private set; } = string.Empty;
    public bool IsActive { get; private set; } = true;
   

    //Navigational properties
    public virtual AccountType? AccountType { get; private set; }
    private readonly List<BankTransaction> _transactions = new List<BankTransaction>();
    public IReadOnlyCollection<BankTransaction> Transactions => _transactions.AsReadOnly();

    public void UpdateAccount(string accountName, string UserId, string accountNumber, bool autoSync, string userName, string password, Guid accountType, Guid institutionId)
    {
        InstitutionId = institutionId;
        AccountTypeId = accountType;
        AccountName = accountName;
        AccountNumber = accountNumber;
        UpdatedBy = UserId;
        UpdatedOn = DateTime.UtcNow;
    }

    public void UpdateCurrentBalance(decimal currentBalance, string userId)
    {
        CurrentBalance = currentBalance;
        UpdatedBy = userId;
        UpdatedOn = DateTime.UtcNow;
    }
    public void AddBankTransaction(BankTransaction bankTransaction)
    {
        if (!Transactions.Any(e => e.TransactionDate == bankTransaction.TransactionDate && e.TransactionType == bankTransaction.TransactionType && e.BankAccountId == e.BankAccountId && e.TransactionDescription == bankTransaction.TransactionDescription && e.TransactionReference == bankTransaction.TransactionReference && e.TransactionAmount == bankTransaction.TransactionAmount))
        {
            _transactions.Add(bankTransaction);
            //Do not update the balance if already transaction with greater date is present
            if (!_transactions.Any(e => e.TransactionDate > bankTransaction.TransactionDate))
                CurrentBalance = bankTransaction.Balance;
            UpdatedBy = bankTransaction.UpdatedBy;
            UpdatedOn = DateTime.UtcNow;

            //Add domain event
            //var bankTransactionAddedDomainEvt = new BankTransactionAddedDomainEvent(bankTransaction);
            //AddDomainEvent(bankTransactionAddedDomainEvt);
        }
    }

    public void UpdateBankTransaction(BankTransaction bankTransaction)
    {
        var transaction = _transactions.FirstOrDefault(e => e.Id == bankTransaction.Id);
        if (transaction != null)
        {
            if (bankTransaction.CategoryId != new Guid())
            {
                transaction?.UpdateCategory(bankTransaction.CategoryId);
            }

            if (bankTransaction.Remarks != null)
            {
                transaction.UpdateRemarks(bankTransaction.Remarks);
            }

        }

        //AddDomainEvent(new BankTransactionAddedDomainEvent(bankTransaction));
    }

    public BankTransaction GetTransactionById(Guid transactionId)
    {
        var transaction = _transactions.FirstOrDefault(t => t.Id == transactionId);
        return transaction;
    }
}
