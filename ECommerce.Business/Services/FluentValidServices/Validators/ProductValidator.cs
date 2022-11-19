using ECommerce.Entities.Concrete;
using ECommerce.Entities.Entities.ShopEntities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Services.FluentValidServices.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name)
                .Length(10,50).
                NotEmpty().WithMessage("İsim alanı boş bırakılamaz!");


            RuleFor(x => x.Price)
                .NotNull().WithMessage("Fiyat boş bırakılamaz!");
               
            RuleFor(x => x.Stock)
                .NotNull().WithMessage("100'den az stoğu olan ürünü ekleyemezsiniz!")
                .NotEmpty()
                .GreaterThanOrEqualTo(100);
        }
    }
}
