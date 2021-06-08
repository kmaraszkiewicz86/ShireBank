using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.Entities
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        public ICollection<Account> Accounts { get; set; }
    }
}
