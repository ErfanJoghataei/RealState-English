using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Globalization;

namespace RealState.Dal.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "رمز عبور باید حداقل 6 کاراکتر باشد")]
        public string Password { get; set; }
      
        public string? PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "ایمیل معتبر نیست")]
        public string? Email { get; set; }

        public DateTime? JoneDate { get; set; } = DateTime.Now;



        public bool IsActive { get; set; }



    }
    public static class DateHelper
    {
        public static string ToPersianDate(this DateTime? date)
        {
            if (date == null) return "-";

            var persian = new PersianCalendar();
            var year = persian.GetYear(date.Value);
            var month = persian.GetMonth(date.Value);
            var day = persian.GetDayOfMonth(date.Value);

            return $"{year}/{month:D2}/{day:D2}";
        }
    }

}
