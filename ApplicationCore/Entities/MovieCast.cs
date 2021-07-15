
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class MovieCast
    {
        //Navigation with Movie
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        
        //Navigation with Cast
        public int CastId { get; set; }
        public Cast Cast { get; set; }
        
        public string Character { get; set; }
        
        
        
        
       
    }
}