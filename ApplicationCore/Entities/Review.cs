using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Review
    {
        //Navigation with Movie
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        //Navigation with User
        public int UserId { get; set; }
        public User User { get; set; }

        public decimal Rating { get; set; }
        public string ReviewText { get; set; }
    }
}
