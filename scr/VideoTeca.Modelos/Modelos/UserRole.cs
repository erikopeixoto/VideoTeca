using Microsoft.AspNetCore.Identity;

namespace VideoTeca.Modelos.Modelos
{
    public class UserRole : IdentityUserRole<int>
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}