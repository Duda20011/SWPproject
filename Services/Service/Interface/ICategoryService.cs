using Services.Entity;
using Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service.Interface
{
    public interface ICategoryService
    {
        Task<Category> CreateCategory(CategoryModel req);
        Task<bool> UpdateCategory(CategoryModel req, int id);
        Task<bool> DeleteCategory(int id);
        Task<Category> GetCategoryById(int id);
        Task<List<Category>> GetCategoriesAsync();
    }
}
