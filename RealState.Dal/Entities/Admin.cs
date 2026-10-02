using System.ComponentModel.DataAnnotations;

namespace RealState.Dal.Entities
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }

   
        public string AdminUserName { get; set; }

        public string AdminPassword { get; set; }

        public bool IsActive { get; set; }

    }
}
