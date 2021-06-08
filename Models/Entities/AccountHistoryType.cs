using System.ComponentModel.DataAnnotations;

namespace Models.Entities
{
    public class AccountHistoryType
    {
        [Key]
        public int AccountHistoryTypeId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
