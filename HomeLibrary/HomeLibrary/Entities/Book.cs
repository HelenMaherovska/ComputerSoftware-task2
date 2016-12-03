using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeLibrary.Entities
{
    public abstract class Book
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Edition { get; set; }
        public int Year { get; set; }
        public int Pages { get; set; }

        public Section Section { get; set; }
        public Estimate Estimate { get; set; }

        public Book()
        {
            Section = new Section();
            Estimate = new Estimate();
        }
    }
}
