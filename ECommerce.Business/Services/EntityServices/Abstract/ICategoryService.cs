using ECommerce.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Services.EntityServices.Abstract
{
    public interface ICategoryService
    {
        public Task<int> Add(Category category);
        public Task<int> Update(Category category);
        public Task<int> Delete(int id);
        public Task<Category> GetById(int id);
        public Task<IList<Category>> GetList(Expression<Func<Category, bool>> filter = null);
        public IList<string> GetListOnlyCatName();
        public Task<Category> GetByName(string name);
        
    }
}
