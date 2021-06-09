using System;
using System.ComponentModel.DataAnnotations;

namespace Models.Entities
{
    public class AccountHistory
    {
        [Key]
        public int AccountHistoryId { get; set; }

        [Required]
        public float AmountOfFunds { get; set; }

        [Required]
        public float AmountOfFoundsAfterOperation { get; set; }

        [Required]
        public DateTime HistoryDate { get; set; }

        [Required]
        public AccountHistoryType AccountHistoryType { get; set; }

        [Required]
        public Account Account { get; set; }
    }
}
