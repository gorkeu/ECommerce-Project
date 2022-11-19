using ECommerce.Business.Services.EntityServices.Abstract;
using ECommerce.DataAccess.ECommerceDbContext.ShopDb.DALs.Abstract;
using ECommerce.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Services.EntityServices.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public async Task<int> Add(Category category)
        {
            var categoryExist = await _categoryDal.Get(x => x.Name == category.Name);
            if (categoryExist != null)
            {
                return -1;
            }
            return await _categoryDal.Add(category);
        }

        public async Task<int> Delete(int id)
        {
            var categoryExist = await GetById(id);
            if (categoryExist == null)
            {
                return -1;
            }

            return await _categoryDal.Delete(categoryExist);
        }

        public async Task<Category> GetById(int id)
        {
            var category = _categoryDal.Get(x => x.Id == id);
            return await category;
        }

        public async Task<Category> GetByName(string name)
        {
            return await _categoryDal.GetByName(name);
        }

        public Task<IList<Category>> GetList(Expression<Func<Category, bool>> filter = null)
        {
            var list = filter == null
                ? _categoryDal.GetList()
                : _categoryDal.GetList(filter);
            return list;
        }

        

        public IList<string> GetListOnlyCatName()
        {
            return _categoryDal.GetListOnlyCatName();
        }

        public async Task<int> Update(Category category)
        {
            return await _categoryDal.Update(category);
        }
    }
}
