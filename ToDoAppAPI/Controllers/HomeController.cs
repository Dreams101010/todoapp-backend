﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoAppDomainLayer.DataObjects;
using ToDoAppDomainLayer.Interfaces.Facades;

namespace ToDoAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ICategoryFacade CategoryFacade;
        private readonly ITaskFacade TaskFacade;

        public HomeController(ICategoryFacade categoryFacade, 
            ITaskFacade taskFacade)
        {
            if (categoryFacade is null)
            {
                throw new ArgumentNullException(nameof(categoryFacade));
            }

            if (taskFacade is null)
            {
                throw new ArgumentNullException(nameof(taskFacade));
            }

            CategoryFacade = categoryFacade;
            TaskFacade = taskFacade;
        }

        [HttpPost]
        [Route("addCategory")]
        public IActionResult AddCategory(Category categoryToAdd)
        {
            int insertedId = CategoryFacade.AddCategory(categoryToAdd);
            return Ok(new { Id = insertedId });
        }

        [HttpPost]
        [Route("editCategory")]
        public IActionResult EditCategory(Category categoryToEdit)
        {
            CategoryFacade.EditCategory(categoryToEdit);
            return Ok();
        }

        [HttpPost]
        [Route("removeCategory")]
        public IActionResult RemoveCategoryById(int id)
        {
            CategoryFacade.RemoveCategoryById(id);
            return Ok();
        }

        [HttpGet]
        [Route("getCategories")]
        public IActionResult GetCategories()
        {
            var categories = CategoryFacade.GetCategories();
            return Ok(new { Categories = categories });
        }

        [HttpPost]
        [Route("addTask")]
        public IActionResult AddTask(ToDoTask taskToAdd)
        {
            int insertedId = TaskFacade.AddTask(taskToAdd);
            return Ok(new { Id = insertedId });
        }

        [HttpPost]
        [Route("editTask")]
        public IActionResult EditTask(ToDoTask taskToEdit)
        {
            TaskFacade.EditTask(taskToEdit);
            return Ok();
        }

        [HttpPost]
        [Route("removeTask")]
        public IActionResult RemoveTaskById(int id)
        {
            TaskFacade.RemoveTaskById(id);
            return Ok();
        }

        [HttpGet]
        [Route("getActiveTasks")]
        public IActionResult GetActiveTasks()
        {
            var tasks = TaskFacade.GetActiveTasks();
            return Ok(new { Tasks = tasks });
        }

        [HttpGet]
        [Route("getActiveTasksByCategoryId")]
        public IActionResult GetActiveTasksByCategoryId(int categoryId)
        {
            var tasks = TaskFacade.GetActiveTasksByCategoryId(categoryId);
            return Ok(new { Tasks = tasks });
        }

        [HttpGet]
        [Route("getCompleteTasks")]
        public IActionResult GetCompleteTasks()
        {
            var tasks = TaskFacade.GetCompletedTasks();
            return Ok(new { Tasks = tasks });
        }

        [HttpGet]
        [Route("getCompleteTasksByCategoryId")]
        public IActionResult GetCompleteTasksByCategoryId(int categoryId)
        {
            var tasks = TaskFacade.GetCompletedTasksByCategoryId(categoryId);
            return Ok(new { Tasks = tasks });
        }

        [HttpGet]
        [Route("getTasksByCategoryId")]
        public IActionResult GetTasksByCategoryId(int categoryId)
        {
            var tasks = TaskFacade.GetTasksByCategoryId(categoryId);
            return Ok(new { Tasks = tasks });
        }
    }
}