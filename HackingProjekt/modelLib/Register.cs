using System.ComponentModel.DataAnnotations;

namespace HackingProjekt.modelLib
{
    public class Register
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage ="Password and confimation password do not match")]
        public string ConfirmPassword { get; set; }

    }
}
