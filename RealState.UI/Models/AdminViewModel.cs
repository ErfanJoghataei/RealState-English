using System.ComponentModel.DataAnnotations;

namespace RealState.UI.Models
{
    public class AdminViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "نام کاربری اجباری است")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "رمز عبور اجباری است")]
        public string Password { get; set; }

        public bool IsActive { get; set; }
    }
}

