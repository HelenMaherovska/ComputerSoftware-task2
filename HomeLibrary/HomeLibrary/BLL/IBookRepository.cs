using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeLibrary.Entities;
using HomeLibrary.Containers;
using static HomeLibrary.Helpers.Enums;

namespace HomeLibrary.BLL
{
    public interface IBookRepository<T>
        where T : Book
    {
        int Add(T book);
        int Edit(T book);
        void Delete(int id, int estimate_id);
        T GetById(int id, string type = null);
        BookContainer<T> GetAll(string type = null, BookContainer<T> section = null, int? sectionId = null);
        //BookContainer<T> Search(string text, BookFilter filter, string type = null, BookContainer<T> section = null, int? sectionId = null);
    }
}
