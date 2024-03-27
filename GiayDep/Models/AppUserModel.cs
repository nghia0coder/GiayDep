using Microsoft.AspNetCore.Identity;

namespace GiayDep.Models
{
    public class AppUserModel : IdentityUser
    {
        public string Address { get; set; }

<<<<<<< Updated upstream
        public virtual ICollection<Order> Orders { get; } = new List<Order>();
=======
        public virtual ICollection<HoaDon> HoaDons { get; } = new List<HoaDon>();
>>>>>>> Stashed changes
    }
}
