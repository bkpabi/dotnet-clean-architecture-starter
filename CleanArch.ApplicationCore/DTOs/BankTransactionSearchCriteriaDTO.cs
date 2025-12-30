using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.ApplicationCore.DTOs
{
    public class BankTransactionSearchCriteriaDTO
    {
        public Guid Id { get; set; }
        public Guid BankAccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ReferenceNumber { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> CategoryList { get; set; } = new List<string>(); 
        public string Category { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public string CategoryType { get; set; } = string.Empty;
        public string TransactionType { get; set; } = string.Empty;
        public Guid TransactionTypeId { get; set; }
    }
}
