using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ECommerce.DataAccess.ECommerceDbContext.ShopDb;
using ECommerce.Entities.Concrete;
using ECommerce.Business.Services.EntityServices.Abstract;
using FluentValidation;
using System.Drawing;
using ECommerce.Entities.Entities.ShopEntities.Concrete.PhotoCapture;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Net;
using NuGet.Protocol;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Data.SqlTypes;
using Microsoft.AspNetCore.Session;
using Newtonsoft.Json;
using ECommerce.Entities.Entities.ShopEntities.Concrete;

namespace ECommerce.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IValidator<Product> _productValidator;
        


        public ProductController(IProductService productService, ICategoryService categoryService, IValidator<Product> productValidator)
        {
            _productService = productService;
            _categoryService = categoryService;
            _productValidator = productValidator;
           

        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            foreach (var item in await _productService.GetListWithCategory())
            {
                using (var ms = new MemoryStream(item.Photo))
                {
                    ViewBag.Photos += Image.FromStream(ms);
                }
            }
            return View(await _productService.GetListWithCategory());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int id)
        {

            // id si 0 a eşit olan hiç bir ürün bulunmamaktadır ve default olarak hiç bir id gelmezse int 0 olarak dönüyor.
            if (id == 0)
            {
                return NotFound();
            }

            var product = await _productService.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {

            ViewBag.categories = await _categoryService.GetList();
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // [Bind("Name,Description,Brand,Category,Genders,Unisex,Price,Stock,BodyParts")] 
        public async Task<IActionResult> Create(Product product, FileUpload fileUpload)
        {
            if (fileUpload.File.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    fileUpload.File.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    product.Photo = fileBytes;
                }
            }

            product.CreationTime = DateTime.Now;
            var result = _productValidator.Validate(product);
            if (!result.IsValid)
            {
                ViewBag.categories = _categoryService.GetList().Result;
                return View(product);
            }

            await _productService.Add(product);

            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            ViewBag.categories = _categoryService.GetList().Result;
            var product = await _productService.GetByIdIncludeCat(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            var oldProduct = await _productService.GetByIdIncludeCat(id);
            var category = await _categoryService.GetById(product.Category.Id);
            var result = _productValidator.Validate(product);
            var productsList = _productService.GetList(x => x.Category.Id == product.Category.Id).Result;
            category.Products = productsList.ToList();
            if (result.IsValid)
            {
                oldProduct.Name = product.Name;
                oldProduct.Category.Id = category.Id;
                oldProduct.Category.Name = category.Name;
                oldProduct.Category.Description = category.Description;
                oldProduct.Category.Products = category.Products;
                oldProduct.Brand = product.Brand;
                oldProduct.Gender = product.Gender;
                oldProduct.Unisex = product.Unisex;
                oldProduct.Price = product.Price;
                oldProduct.Stock = product.Stock;
                oldProduct.BodyPart = product.BodyPart;

                var resultation = await _productService.Update(oldProduct);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    Problem($"{error}");
                }
                ViewBag.categories = _categoryService.GetList().Result;
                return View(oldProduct);

            }
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var product = await _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var product = await _productService.GetById(id);
            if (product != null)
            {
                await _productService.Delete(id);
            }


            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> NewProducts()
        {
            return View(await _productService.GetList());
        }

        public async Task<IActionResult> UpperProducts()
        {
            return View(await _productService.GetList(x => x.BodyPart == Entities.Entities.ShopEntities.Concrete.Enum.BodyParts.Upper));
        }

        public async Task<IActionResult> LowerProducts()
        {
            return View(await _productService.GetList(x => x.BodyPart == Entities.Entities.ShopEntities.Concrete.Enum.BodyParts.Lower));
        }

        public async Task<IActionResult> ShoeProducts()
        {
            return View(await _productService.GetList(x => x.BodyPart == Entities.Entities.ShopEntities.Concrete.Enum.BodyParts.Shoe));
        }

        public async Task<IActionResult> AddToCart(int Id)
        {
            
            if(HttpContext.Session.GetString("products") == null)
            {
                List<int> list = new List<int>();
                list.Add(Id);
                HttpContext.Session.SetString("products", JsonConvert.SerializeObject(list));
                return RedirectToAction("NewProducts");
            }
            else
            {
                List<int> list = JsonConvert.DeserializeObject<List<int>>(HttpContext.Session.GetString("products"));
                list.Add(Id);
                HttpContext.Session.SetString("products", JsonConvert.SerializeObject(list));
                return RedirectToAction("NewProducts");
            }
        }

        public async Task<IActionResult> Cart()
        {
            if (HttpContext.Session.GetString("products") == null)
            {
                return View();
            }
            else
            {
                List<Product> list = new List<Product>();
                List<int> list1 = JsonConvert.DeserializeObject<List<int>>(HttpContext.Session.GetString("products"));

                foreach (var id in list1)
                {
                    Product product = await _productService.GetById(id);
                    list.Add(product);
                }
                return View(list);
            }
        }
        
        public IActionResult Payment(decimal sum)
        {
            return View(sum);
        }

    }
}
