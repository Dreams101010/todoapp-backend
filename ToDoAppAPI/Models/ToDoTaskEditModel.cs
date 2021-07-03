using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ToDoAppDomainLayer.DataObjects;

namespace ToDoAppAPI.Models
{
    public class ToDoTaskEditModel
    {
        [Required(ErrorMessage = "The id of task is required")]
        public int? Id { get; set; }
        [Required(ErrorMessage = "The title of task is required")]
        [MaxLength(300, ErrorMessage = "Length of title cannot exceed 300 characters")]
        [MinLength(1, ErrorMessage = "Title cannot be empty")]
        public string Title { get; set; }
        [Required(ErrorMessage = "The description of task is required")]
        [MaxLength(1000, ErrorMessage = "Length of description cannot exceed 1000 characters")]
        [MinLength(1, ErrorMessage = "Description cannot be empty")]
        public string Description { get; set; }
        [Required(ErrorMessage = "The creation date of task is required")]
        public DateTime? CreatedAt { get; set; }
        [Required(ErrorMessage = "The active flag of task is required")]
        public bool? IsActive { get; set; }
        [Required(ErrorMessage = "The complete flag of task is required")]
        public bool? IsComplete { get; set; }
        [Required(ErrorMessage = "The id of category to which task belongs to is required")]
        public int? CategoryId { get; set; }

        public ToDoTask ConvertToDomainModel()
        {
            return new()
            {
                Id = this.Id.Value,
                Title = this.Title,
                Description = this.Description,
                CreatedAt = this.CreatedAt.Value,
                IsActive = this.IsActive.Value,
                IsComplete = this.IsComplete.Value,
                CategoryId = this.CategoryId.Value
            };
        }
    }
}
