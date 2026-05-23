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
        public static async Task Main(string[] args)
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
            builder.Services.AddScoped<INotificationRepo, NotificationRepo>();
            builder.Services.AddScoped<IPostRepo, PostRepo>();
            builder.Services.AddScoped<IUserRepo, UserRepo>();
            builder.Services.AddScoped<ICartRepo, CartRepo>();
            builder.Services.AddScoped<IVoteRepo, VoteRepo>();
            builder.Services.AddScoped<IPaymentRepo, PaymentRepo>();




            // Services
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();
            builder.Services.AddScoped<IReferencePlanetService, ReferencePlanetService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddScoped<INotificationService, NotificationService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddScoped<IVoteService, VoteService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();

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

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication(); 
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            await SeedRolesAsync(app.Services);
            app.Run();
        }
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Admin", "User", "USER" }; // Added 'USER' to safely match your current registration string

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    // Create the roles and seed them into the database
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
}
