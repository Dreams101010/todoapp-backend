using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ToDoAppDomainLayer.DataObjects;

namespace ToDoAppAPI.Models
{
    public class CategoryAddModel
    {
        [Required(ErrorMessage = "The title of category is required")]
        [MaxLength(300, ErrorMessage = "Length of title cannot exceed 300 characters")]
        [MinLength(1, ErrorMessage = "Title cannot be empty")]
        public string Title { get; set; }
        [Required(ErrorMessage = "The color string of category is required")]
        [RegularExpression(@"#[a-f|A-F|\d]{6}", ErrorMessage = "The color string must be in correct format")]
        public string Color { get; set; }

        public Category ConvertToDomainModel()
        {
            return new()
            {
                Title = this.Title,
                Color = this.Color,
            };
        }
    }
}
