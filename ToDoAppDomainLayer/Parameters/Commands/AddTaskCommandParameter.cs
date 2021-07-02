using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAppDomainLayer.DataObjects;

namespace ToDoAppDomainLayer.Parameters.Commands
{
    public class AddTaskCommandParameter
    {
        public ToDoTask TaskToAdd { get; set; }
    }
}
