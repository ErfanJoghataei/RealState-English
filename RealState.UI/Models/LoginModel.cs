using System.ComponentModel.DataAnnotations;

namespace RealState.UI.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "لطفاً نام کاربری را وارد کنید")]

        public string UserName { get; set; }

        [Required(ErrorMessage = "لطفاً رمز عبور را وارد کنید")]
        public string Password { get; set; }
    }
}
