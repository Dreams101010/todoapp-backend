using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoAppDomainLayer.Exceptions
{
    public class DatabaseInteractionException : Exception
    {
        public DatabaseInteractionException(string message) : base(message)
        {

        }

        public DatabaseInteractionException(string message, Exception innerException) 
            : base(message, innerException)
        {

        }
    }
}
