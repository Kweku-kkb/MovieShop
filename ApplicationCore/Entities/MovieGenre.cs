
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
    
    public class MovieGenre
    {
        //navigation with genre
        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        //Navigation with Movie
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
