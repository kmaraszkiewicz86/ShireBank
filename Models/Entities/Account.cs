using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.Entities
{
    public class Account
    {
        [Key]
        public uint AccountId { get; set; }

        [Required]
        public float Amount { get; set; }

        [Required]
        public float DebitLimit { get; set; }

        [Required]
        public Customer Customer { get; set; }

        public ICollection<AccountHistory> AccountHistories { get; set; }
    }
}
