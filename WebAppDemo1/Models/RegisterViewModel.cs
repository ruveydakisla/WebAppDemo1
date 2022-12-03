using System.ComponentModel.DataAnnotations;

namespace WebAppDemo1.Models
{
    public class RegisterViewModel:LoginViewModel
    {
        [Required]
        [Compare(nameof(Password))]
       public string RePassword{get; set;}
    }
}
