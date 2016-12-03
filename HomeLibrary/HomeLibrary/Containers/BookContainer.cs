using HomeLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeLibrary.Containers
{
    public abstract class BookContainer<T>
        where T : Book
    {
        public List<T> Books { get; set; }
        public int BooksCount { get; set; }

        public BookContainer()
        {
            Books = new List<T>();
        }
    }
}
