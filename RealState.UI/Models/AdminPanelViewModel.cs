using RealState.Dal.Entities;

namespace RealState.UI.Models
{
    public class AdminPanelViewModel
    {
        // لیست همه ادمین‌ها
        public List<AdminViewModel> Admins { get; set; } = new List<AdminViewModel>();

        // برای عملیات افزودن یا ویرایش یک ادمین
        public AdminViewModel SelectedAdmin { get; set; } = new AdminViewModel();

        public List<PropertyViewModel> Properties { get; set; } = new List<PropertyViewModel> ();

       
    }
}
