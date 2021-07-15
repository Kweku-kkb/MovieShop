using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class UserRole
    {
        //navigation with UserID
        public int UserId { get; set; }
        public User User { get; set; }
        
        //Navigation with Role
        public int RoleId { get; set; }
        public Role Role { get; set; }
        
    }
}
