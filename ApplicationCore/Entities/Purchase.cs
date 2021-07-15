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
    [Table("Purchase")]
    public class Purchase
    {
        [Key]
        public int Id { get; set; }
        //Navigation with User
        public int UserId { get; set; }
        public User User { get; set; }

        public System.Guid PurchaseNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime PurchaseDateTime { get; set; }

        
       
        //Navigation with Movie
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
