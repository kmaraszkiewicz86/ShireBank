using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.Entities
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public Customer Customer { get; set; }

        public ICollection<AccountHistory> AccountHistories { get; set; }
    }
}
