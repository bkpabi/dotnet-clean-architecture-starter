using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.ApplicationCore.DTOs;

public class BankAccountDTO : BaseDTO
{
    public string AccountName { get; set; } = string.Empty;
    public string? AccountNumber { get; set; } = string.Empty;
    public Guid InstitutionId { get; set; }
    public Guid AccountTypeId { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal CurrentBalance { get; set; }
    public string UserId { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public string AccountTypeName { get; set; } = string.Empty;
    public string InstitutionName { get; set; } = string.Empty;
}
