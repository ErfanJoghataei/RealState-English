using System.ComponentModel.DataAnnotations;

namespace RealState.UI.Models
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "نام کاربری الزامی است")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "رمز عبور الزامی است")]
        [MinLength(6, ErrorMessage = "رمز عبور باید حداقل ۶ کاراکتر باشد")]
        public string Password { get; set; }

        // بدون Required → کاملاً اختیاری
        public string? PhoneNumber { get; set; }

        // فقط اگه پر شد باید درست باشه
        [EmailAddress(ErrorMessage = "ایمیل معتبر نیست")]
        public string? Email { get; set; }


    }
}
