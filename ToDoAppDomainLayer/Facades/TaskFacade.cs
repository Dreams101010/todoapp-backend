using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAppDomainLayer.Interfaces.Facades;
using ToDoAppDomainLayer.Interfaces;
using ToDoAppDomainLayer.Parameters.Commands;
using ToDoAppDomainLayer.Parameters.Queries;
using ToDoAppDomainLayer.DataObjects;


namespace ToDoAppDomainLayer.Facades
{
    using AddTaskCommand = ICommand<AddTaskCommandParameter, int>;
    using EditTaskCommand = ICommand<EditTaskCommandParameter, int>;
    using RemoveTaskCommand = ICommand<RemoveTaskCommandParameter, int>;
    using GetTasksByCategoryIdQuery = IQuery<GetTasksByCategoryIdQueryParameter,
            IEnumerable<ToDoTaskOutputModel>>;
    using GetActiveTasksQuery = IQuery<GetActiveTasksQueryParameter,
            IEnumerable<ToDoTaskOutputModel>>;
    using GetActiveTasksByCategoryIdQuery = IQuery<GetActiveTasksByCategoryIdQueryParameter,
            IEnumerable<ToDoTaskOutputModel>>;
    using GetCompleteTasksQuery = IQuery<GetCompleteTasksQueryParameter,
            IEnumerable<ToDoTaskOutputModel>>;
    using GetCompleteTasksByCategoryIdQuery = IQuery<GetCompleteTasksByCategoryIdQueryParameter,
            IEnumerable<ToDoTaskOutputModel>>;
    public class TaskFacade : ITaskFacade
    {
        private readonly AddTaskCommand AddTaskCommand = null;
        private readonly EditTaskCommand EditTaskCommand = null;
        private readonly RemoveTaskCommand RemoveTaskCommand = null;
        private readonly GetTasksByCategoryIdQuery GetTasksByCategoryIdQuery = null;
        private readonly GetActiveTasksQuery GetActiveTasksQuery = null;
        private readonly GetActiveTasksByCategoryIdQuery GetActiveTasksByCategoryIdQuery = null;
        private readonly GetCompleteTasksQuery GetCompleteTasksQuery = null;
        private readonly GetCompleteTasksByCategoryIdQuery GetCompleteTasksByCategoryIdQuery = null;

        public TaskFacade(
            AddTaskCommand addTaskCommand,
            EditTaskCommand editTaskCommand,
            RemoveTaskCommand removeTaskCommand,
            GetTasksByCategoryIdQuery getTasksByCategoryIdQuery,
            GetActiveTasksQuery getActiveTasksQuery,
            GetActiveTasksByCategoryIdQuery getActiveTasksByCategoryIdQuery,
            GetCompleteTasksQuery getCompleteTasksQuery,
            GetCompleteTasksByCategoryIdQuery GetCompleteTasksByCategoryIdQuery)
        {
            if (addTaskCommand is null)
            {
                throw new ArgumentNullException(nameof(addTaskCommand));
            }

            if (editTaskCommand is null)
            {
                throw new ArgumentNullException(nameof(editTaskCommand));
            }

            if (removeTaskCommand is null)
            {
                throw new ArgumentNullException(nameof(removeTaskCommand));
            }

            if (getTasksByCategoryIdQuery is null)
            {
                throw new ArgumentNullException(nameof(getTasksByCategoryIdQuery));
            }

            if (getActiveTasksQuery is null)
            {
                throw new ArgumentNullException(nameof(getActiveTasksQuery));
            }

            if (getActiveTasksByCategoryIdQuery is null)
            {
                throw new ArgumentNullException(nameof(getActiveTasksByCategoryIdQuery));
            }

            if (getCompleteTasksQuery is null)
            {
                throw new ArgumentNullException(nameof(getCompleteTasksQuery));
            }

            if (GetCompleteTasksByCategoryIdQuery is null)
            {
                throw new ArgumentNullException(nameof(GetCompleteTasksByCategoryIdQuery));
            }
            this.AddTaskCommand = addTaskCommand;
            this.EditTaskCommand = editTaskCommand;
            this.RemoveTaskCommand = removeTaskCommand;
            this.GetTasksByCategoryIdQuery = getTasksByCategoryIdQuery;
            this.GetActiveTasksQuery = getActiveTasksQuery;
            this.GetActiveTasksByCategoryIdQuery = getActiveTasksByCategoryIdQuery;
            this.GetCompleteTasksQuery = getCompleteTasksQuery;
            this.GetCompleteTasksByCategoryIdQuery = GetCompleteTasksByCategoryIdQuery;
        }

        public int AddTask(ToDoTask taskToAdd)
        {
            if (taskToAdd is null)
            {
                throw new ArgumentNullException(nameof(taskToAdd));
            }

            AddTaskCommandParameter param = new()
            {
                TaskToAdd = taskToAdd,
            };
            return AddTaskCommand.Execute(param);
        }
        public void EditTask(ToDoTask taskToEdit)
        {
            if (taskToEdit is null)
            {
                throw new ArgumentNullException(nameof(taskToEdit));
            }

            EditTaskCommandParameter param = new()
            {
                Task = taskToEdit,
            };
            EditTaskCommand.Execute(param);
        }

        public void RemoveTaskById(int taskId)
        {
            RemoveTaskCommandParameter param = new()
            {
                TaskId = taskId,
            };
            RemoveTaskCommand.Execute(param);
        }

        public IEnumerable<ToDoTaskOutputModel> GetTasksByCategoryId(int categoryId)
        {
            GetTasksByCategoryIdQueryParameter param = new()
            {
                CategoryId = categoryId,
            };
            return GetTasksByCategoryIdQuery.Execute(param);
        }

        public IEnumerable<ToDoTaskOutputModel> GetActiveTasks()
        {
            GetActiveTasksQueryParameter param = new();
            return GetActiveTasksQuery.Execute(param);
        }
        public IEnumerable<ToDoTaskOutputModel> GetActiveTasksByCategoryId(int categoryId)
        {
            GetActiveTasksByCategoryIdQueryParameter param = new()
            {
                CategoryId = categoryId,
            };
            return GetActiveTasksByCategoryIdQuery.Execute(param);
        }
        public IEnumerable<ToDoTaskOutputModel> GetCompletedTasks()
        {
            GetCompleteTasksQueryParameter param = new();
            return GetCompleteTasksQuery.Execute(param);
        }
        public IEnumerable<ToDoTaskOutputModel> GetCompletedTasksByCategoryId(int categoryId)
        {
            GetCompleteTasksByCategoryIdQueryParameter param = new()
            {
                CategoryId = categoryId,
            };
            return GetCompleteTasksByCategoryIdQuery.Execute(param);
        }
    }
}
