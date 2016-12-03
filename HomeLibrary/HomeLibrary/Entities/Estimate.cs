using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeLibrary.Entities
{
    public class Estimate
    {
        public int Id { get; set; }
        public string Origin { get; set; }
        public bool Availability { get; set; }
        public string Worth { get; set; }
        public string Recommendation { get; set; }
    }
}
