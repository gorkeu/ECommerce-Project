using ECommerce.Business.Services.EntityServices.Abstract;
using ECommerce.DataAccess.ECommerceDbContext.ShopDb;
using ECommerce.Business.Services.EntityServices.Concrete;
using ECommerce.DataAccess.ECommerceDbContext.ShopDb.DALs.Concrete;
using ECommerce.DataAccess.ECommerceDbContext.ShopDb.DALs.Abstract;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using ECommerce.Business.Services.AutofacServices;
using FluentValidation.AspNetCore;
using ECommerce.Business.Services.FluentValidServices.Validators;
using ECommerce.Entities.Entities.UserEntities.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ECommerceContext>();
builder.Services.AddIdentity<User, Role>().AddEntityFrameworkStores<ECommerceContext>();
builder.Services.AddSession();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new AutofacServiceAddModule());
});

builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductValidator>());
builder.Services.AddControllersWithViews().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    o.JsonSerializerOptions.PropertyNamingPolicy = null;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
