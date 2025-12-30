using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.ApplicationCore.DTOs;

public class BankTransactionDTO
{
    public Guid BankAccountId { get; set; }
    public string BankName { get; set; } = string.Empty;
    public DateTime TransactionDate { get; set; }
    public string? TransactionReference { get; set; } = string.Empty;
    public string TransactionDescription { get; set; } = string.Empty;
    public Guid TransactionTypeId { get; set; }
    public string? TransactionType { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal TransactionAmount { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Balance { get; set; }
    public Guid CategoryId { get; set; }
    public string? Category { get; set; }
    public string? Remarks { get; set; } = string.Empty;
}
