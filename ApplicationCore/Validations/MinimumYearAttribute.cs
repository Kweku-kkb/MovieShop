using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Validations
{
    public class MinimumYearAttribute : ValidationAttribute
    {
        public int Year { get;}
        public MinimumYearAttribute(int year)
        {
            year = Year;
        }
    }
}
