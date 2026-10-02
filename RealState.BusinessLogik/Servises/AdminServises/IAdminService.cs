using RealState.Dal.Entities;
using System.Net.NetworkInformation;
namespace RealState.BusinessLogik.Servises.AdminServises
{
    public interface IAdminService
    {
        Task<Admin> AddAdminAsync(string username,  string password);
         Task<Admin> RemoveAdminAsync(int id);
        Task<Admin> EditAdminAsync(int id,string username,string password);

        List<Admin> GetAllAdmins();
    }


}
