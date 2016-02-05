

using System.ComponentModel.DataAnnotations;

namespace ForumDisscussion.Models
{
    public class LoginModel
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username Required!")]
        public string UserName { get; set; }
       [Required(ErrorMessage = "Password Required!")]

        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}