using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeLibrary.Entities
{
    public class Schoolbook : Book
    {
        public int Form { get; set; }
        public string Subject { get; set; }
    }
}
