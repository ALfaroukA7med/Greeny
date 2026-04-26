using Greeny.BLL.Admin.Services.Interfaces;
using Greeny.DAL.Database;
using Greeny.DAL.Entities;
using Greeny.DAL.Repository.Implementation;
using Greeny.DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Greeny.BLL.Admin.Services.Implementation;


namespace Greeny.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var config = builder.Configuration.GetConnectionString("ProjectConnection");
            builder.Services.AddDbContext<GreenyDbContext>(options =>
                options.UseSqlServer(config));


            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;            
                options.Password.RequireLowercase = false;       
                options.Password.RequireUppercase = false;        
                options.Password.RequireNonAlphanumeric = false; 
                options.Password.RequiredLength = 6;              
            })
            .AddEntityFrameworkStores<GreenyDbContext>();


            // Repositories
            builder.Services.AddScoped<IProductRepo, ProductRepo>();
            builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
            builder.Services.AddScoped<IReviewRepo,ReviewRepo>();

            // Services
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();


            //AutoMapper
            builder.Services.AddAutoMapper(cfg =>
            {

            }, typeof(ProductProfile).Assembly);

            builder.Services.AddAutoMapper(cfg =>
            {

            }, typeof(CategoryProfile).Assembly);

            builder.Services.AddAutoMapper(cfg =>
            {

            }, typeof(ReviewProfile).Assembly);



            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();



            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication(); 
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
