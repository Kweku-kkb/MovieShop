using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Entities
{
    //[Table("User")]
    public class User
    {
        //[Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
        public string PhoneNumber { get; set; }
        public bool? TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDate { get; set; }
        public DateTime? LastLoginDateTime { get; set; }
        public bool? IsLocked { get; set; }
        public int? AccessFailedCount { get; set; }


        //navigation for Review
        public ICollection<Review> Reviews { get; set; }
        
        //navigation for Favorites
        public ICollection<Favorite> Favorites { get; set; }
        
        //navigation for Purchase
        public ICollection<Purchase> Purchases { get; set; }

        //navigation for Role
        public ICollection<Role> Roles { get; set; }
        
    }
}
