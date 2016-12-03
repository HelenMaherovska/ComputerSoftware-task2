using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeLibrary.BLL
{
    public abstract class AbstractRepository
    {
        protected readonly string _connectionString;

        public AbstractRepository()
        {
            _connectionString = @"Server=.\SQLEXPRESS;Database=Library;Integrated Security=SSPI;";
        }
    }
}
