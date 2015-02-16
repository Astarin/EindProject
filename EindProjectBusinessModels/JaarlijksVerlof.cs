using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EindProjectBusinessModels
{
    public class JaarlijksVerlof
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        public int Jaar { get; set; }
        public int AantalDagen { get; set; }
    }
}
