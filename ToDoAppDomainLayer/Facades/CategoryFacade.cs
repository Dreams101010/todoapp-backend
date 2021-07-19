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
    public class CategoryFacade : ICategoryFacade
    {
        private readonly ICommand<AddCategoryCommandParameter, int> 
            AddCategoryCommand = null;
        private readonly ICommand<EditCategoryCommandParameter, int> 
            EditCategoryCommand = null;
        private readonly ICommand<RemoveCategoryCommandParameter, int> 
            RemoveCategoryCommand = null;
        private readonly IQuery<GetCategoriesQueryParameter, IEnumerable<Category>> 
            GetCategoriesQuery = null;
        private readonly IQuery<GetCategoryByIdQueryParameter, Category>
            GetCategoryByIdQuery = null;
        public CategoryFacade(
            ICommand<AddCategoryCommandParameter, int> addCommand,
            ICommand<EditCategoryCommandParameter, int> editCommand,
            ICommand<RemoveCategoryCommandParameter, int> removeCommand,
            IQuery<GetCategoriesQueryParameter, IEnumerable<Category>> getCategoriesQuery,
            IQuery<GetCategoryByIdQueryParameter, Category> getCategoryByIdQuery)
        {
            this.AddCategoryCommand = addCommand ?? throw new ArgumentNullException(nameof(addCommand));
            this.EditCategoryCommand = editCommand ?? throw new ArgumentNullException(nameof(editCommand));
            this.RemoveCategoryCommand = removeCommand ?? throw new ArgumentNullException(nameof(removeCommand));
            this.GetCategoriesQuery = getCategoriesQuery ?? throw new ArgumentNullException(nameof(getCategoriesQuery));
            this.GetCategoryByIdQuery = getCategoryByIdQuery ?? throw new ArgumentNullException(nameof(getCategoryByIdQuery));
        }
        public int AddCategory(Category categoryToAdd)
        {
            if (categoryToAdd is null)
            {
                throw new ArgumentNullException(nameof(categoryToAdd));
            }

            AddCategoryCommandParameter param = new()
            {
                CategoryToAdd = categoryToAdd,
            };
            return AddCategoryCommand.Execute(param);
        }

        public void EditCategory(Category categoryToEdit)
        {
            if (categoryToEdit is null)
            {
                throw new ArgumentNullException(nameof(categoryToEdit));
            }

            EditCategoryCommandParameter param = new()
            {
                Category = categoryToEdit,
            };
            EditCategoryCommand.Execute(param);
        }

        public void RemoveCategoryById(int categoryId)
        {
            RemoveCategoryCommandParameter param = new()
            {
                CategoryId = categoryId,
            };
            RemoveCategoryCommand.Execute(param);
        }

        public IEnumerable<Category> GetCategories()
        {
            GetCategoriesQueryParameter param = new();
            return GetCategoriesQuery.Execute(param);
        }

        public Category GetCategoryById(int id)
        {
            GetCategoryByIdQueryParameter param = new()
            {
                Id = id,
            };
            return GetCategoryByIdQuery.Execute(param);
        }
    }
}
