using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoAppDomainLayer.DataObjects
{
    public class ToDoTaskOutputModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public bool IsComplete { get; set; }
        public int CategoryId { get; set; }
        // embedded related category data
        public string CategoryTitle { get; set; }
        public string CategoryColor { get; set; }
    }
}
