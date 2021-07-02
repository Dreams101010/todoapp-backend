using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAppDomainLayer.Interfaces;
using ToDoAppDomainLayer.DataObjects;

namespace ToDoAppDomainLayer.Interfaces.Facades
{
    public interface ICategoryFacade
    {
        public int AddCategory(Category categoryToAdd);
        public void EditCategory(Category categoryToEdit);
        public void RemoveCategoryById(int categoryId);
        public IEnumerable<Category> GetCategories();
    }
}
