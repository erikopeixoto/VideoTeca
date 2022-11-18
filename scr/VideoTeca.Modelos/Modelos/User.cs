using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace VideoTeca.Modelos.Modelos
{
    public class User : IdentityUser<int>
    {
        [Column(TypeName = "nvarchar(150)")]
        public string Password { get; set; }
        public string FullName { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}