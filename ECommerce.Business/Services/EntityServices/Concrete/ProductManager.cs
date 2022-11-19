using ECommerce.Business.Services.EntityServices.Abstract;
using ECommerce.Business.Services.FluentValidServices.Validators;
using ECommerce.DataAccess.ECommerceDbContext.ShopDb.DALs.Abstract;
using ECommerce.Entities.Concrete;
using ECommerce.Entities.Entities.ShopEntities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Services.EntityServices.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;
        
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
            
        }
        public async Task<int> Add(Product product)
        {
            var productExist = await _productDal.Get(x => x.Name == product.Name && x.Brand == product.Brand && x.Category == product.Category);
            if (productExist != null)
            {
                return -1;
            }
            var difference = DateTime.Now.Subtract(product.CreationTime).Days;
            if (difference < 30)
            {
                product.NewOrNot = true;
            }
            return await _productDal.Add(product);
        }

        public async Task<int> Delete(int id)
        {
            var product = await GetById(id);
            if (product == null)
            {
                return -1;
            }
            return await _productDal.Delete(product);
        }

        public async Task<Product> GetById(int id)
        {
            return await _productDal.Get(x => x.Id == id);
        }

        public Task<IList<Product>> GetList(Expression<Func<Product, bool>> filter = null)
        {
            var list = filter == null 
                ? _productDal.GetList() 
                : _productDal.GetList(filter);
            return list;
        }

        public async Task<IList<Product>> GetListWithCategory()
        {
            var list = await _productDal.GetListIncludeCategory();
            return list;
        }

        public async Task<IList<Product>> GetListByCategory(int catId)
        {
            var list = await _productDal.GetList(x => x.Category.Id == catId);
            return list;
        }

        public async Task<int> Update(Product product)
        {
            return await _productDal.Update(product);
        }

        public async Task<Product> GetByIdIncludeCat(int id)
        {
            var product = await _productDal.GetByIdIncludeCat(id);
            return product;
        }
    }
}
