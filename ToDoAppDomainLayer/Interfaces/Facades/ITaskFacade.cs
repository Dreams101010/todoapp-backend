using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAppDomainLayer.DataObjects;

namespace ToDoAppDomainLayer.Interfaces.Facades
{
    public interface ITaskFacade
    {
        public int AddTask(ToDoTask taskToAdd);
        public void EditTask(ToDoTask taskToEdit);
        public void RemoveTaskById(int taskId);
        public IEnumerable<ToDoTaskOutputModel> GetTasksByCategoryId(int categoryId);
        public IEnumerable<ToDoTaskOutputModel> GetActiveTasks();
        public IEnumerable<ToDoTaskOutputModel> GetActiveTasksByCategoryId(int categoryId);
        public IEnumerable<ToDoTaskOutputModel> GetCompletedTasks();
        public IEnumerable<ToDoTaskOutputModel> GetCompletedTasksByCategoryId(int categoryId);
    }
}
