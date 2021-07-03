using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoAppDomainLayer.Exceptions
{
    public class DatabaseEntryNotFoundException : Exception
    {
        public DatabaseEntryNotFoundException(string message) : base(message)
        {

        }

        public DatabaseEntryNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
