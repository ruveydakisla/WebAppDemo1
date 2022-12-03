using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppDemo1.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        
        public string? NameSurname { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [StringLength(100)]
        public string Password { get; set; }
        public bool Locked { get; set; } = false;

        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}

