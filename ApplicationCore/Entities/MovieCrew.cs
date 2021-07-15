using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    
    public class MovieCrew
    {
        //Navigation with Movie
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        
        //navigation for crew
        public int CrewId { get; set; }
        public Crew Crew { get; set; }
        public string Department { get; set; }
        public string Job { get; set; }

        

        
    }
}
