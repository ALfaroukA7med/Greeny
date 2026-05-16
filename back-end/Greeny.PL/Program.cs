using Greeny.DAL.Database;
using Greeny.DAL.Entities;
using Greeny.DAL.Repository.Implementation;
using Greeny.DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Greeny.BLL.Services.Implementation;
using Greeny.BLL.Services.Interfaces;


namespace Greeny.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // don't remove this.
            builder.Configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

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
            builder.Services.AddScoped<IReferencePlanetRepo, ReferencePlanetRepo>();
            builder.Services.AddScoped<ICommentRepo, CommentRepo>();
            builder.Services.AddScoped<IPostRepo, PostRepo>();
            builder.Services.AddScoped<IUserRepo, UserRepo>();




            // Services
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();
            builder.Services.AddScoped<IReferencePlanetService, ReferencePlanetService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddScoped<IUserService, UserService>();



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

            builder.Services.AddAutoMapper(cfg =>
            {

            }, typeof(RefPlanet).Assembly);


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
