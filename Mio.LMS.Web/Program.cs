    using Microsoft.EntityFrameworkCore;
    using Mio.LMS.Web.Models;
    using Mio.LMS.Web.Repositories;
    using Mio.LMS.Web.Repositories.Impl;
    using Mio.LMS.Web.UnitOfWorks;
    using Mio.LMS.Web.UnitOfWorks.Impl;

    namespace Mio.LMS.Web;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
            
            if (connectionString == string.Empty)
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            }
            
            // Cấu hình môi trường Dev: setx ASPNETCORE_ENVIRONMENT "Development"
            
            // Add services to the container.
            builder.Services.AddDbContext<LMSDbContext>(options =>
                options.UseMySql(
                    connectionString,
                    ServerVersion.AutoDetect(connectionString)
                )
            );
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<ICourseRepository, CourseRepository>();
            builder.Services.AddTransient<ISectionRepository, SectionRepository>();
            builder.Services.AddTransient<ILessonRepository, LessonRepository>();
            
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

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

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }