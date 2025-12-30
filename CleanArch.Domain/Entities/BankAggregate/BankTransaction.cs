using CleanArch.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Domain.Entities.BankAggregate;

public class BankTransaction : BaseEntity
{
    public BankTransaction() { }

    public BankTransaction(Guid bankAccountId
        , DateTime transactionDate
        , string transactionRef
        , string transactionDesc
        , Guid transactionTypeId
        , decimal transactionAmount
        , decimal balance
        , Guid categoryId
        , string remarks)
    {
        BankAccountId = bankAccountId;
        TransactionDate = transactionDate;
        TransactionReference = transactionRef;
        TransactionDescription = transactionDesc;
        TransactionTypeId = transactionTypeId;
        TransactionAmount = transactionAmount;
        Balance = balance;
        CategoryId = categoryId;
        Remarks = remarks;
    }

    public Guid BankAccountId { get; private set; }
    public DateTime TransactionDate { get; private set; }
    public string? TransactionReference { get; private set; } = string.Empty;
    public string TransactionDescription { get; private set; } = string.Empty;
    public Guid TransactionTypeId { get; private set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal TransactionAmount { get; private set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Balance { get; private set; }
    public Guid CategoryId { get; private set; }
    public string? Remarks { get; private set; } = string.Empty;

    //Navigational Properties
    public virtual TransactionType? TransactionType { get; private set; }
    public virtual Category? Category { get; private set; }
    public virtual BankAccount? BankAccount { get; private set; }

    public void UpdateCategory(Guid categoryId)
    {
        CategoryId = categoryId;
    }

    public void UpdateRemarks(string remarks)
    {
        if (Remarks != remarks)
            Remarks = remarks;
    }

}
