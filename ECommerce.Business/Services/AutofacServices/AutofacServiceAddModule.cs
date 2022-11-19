using Autofac;
using ECommerce.Business.Services.EntityServices.Abstract;
using ECommerce.Business.Services.EntityServices.Concrete;
using ECommerce.Business.Services.FluentValidServices.Validators;
using ECommerce.DataAccess.ECommerceDbContext.ShopDb.DALs.Abstract;
using ECommerce.DataAccess.ECommerceDbContext.ShopDb.DALs.Concrete;
using ECommerce.Entities.Concrete;
using ECommerce.Entities.Entities.ShopEntities.Concrete;
using ECommerce.Entities.Entities.UserEntities.Concrete.Dtos;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Services.AutofacServices
{
    public class AutofacServiceAddModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Product and Category managers service configuration
            builder.RegisterType<ProductDal>().As<IProductDal>();
            builder.RegisterType<ProductManager>().As<IProductService>();
            builder.RegisterType<CategoryDal>().As<ICategoryDal>();
            builder.RegisterType<CategoryManager>().As<ICategoryService>();
            // User Service configuration
            builder.RegisterType<UserDal>().As<IUserDal>();
            builder.RegisterType<UserManager>().As<IUserService>();

            //Product and User FluentValidation service configuration
            builder.RegisterType<ProductValidator>().As<IValidator<Product>>();
            builder.RegisterType<UserValidator>().As<IValidator<UserRegisterDto>>();

            // This class is for image capture and is needed in order to capture the files on binding
            builder.RegisterType<FormFile>().As<IFormFile>();



        }
    }
}
