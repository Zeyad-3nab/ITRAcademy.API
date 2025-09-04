
using ITR.API.BLL.Data.Contexts;
using ITR.API.BLL.Repositories;
using ITR.API.DAL.Models;
using ITR.API.DAL.Repositories;
using ITR.API.DAL.Services;
using ITR.API.Services;
using ITRProject.API.PL.DataSeed;
using ITRProject.API.PL.Extention;
using ITRProject.API.PL.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace ITRProject.API.PL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            var builder = WebApplication.CreateBuilder(args);

            //Allow all people
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.SetIsOriginAllowed(_ => true)
                                      .AllowAnyMethod()
                                      .AllowAnyHeader()
                                      .AllowCredentials();
                                  });
            });

            // Add services to the container.



            builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();



            //Connection String
            builder.Services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();         //Allow DI to Store

            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(M => M.AddProfile(new ApplicationProfile(builder.Configuration)));


            
            
            builder.Services.AddHttpClient<VodService>();



            builder.Services.AddSwaggerGenJwtAuth();
            builder.Services.AddCustomJwtAuth(builder.Configuration);
            var app = builder.Build();



            //Apply Migrtion
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();   // ·Ê ›Ì Migration ÂÌ ÿ»ﬁ √Ê Ê„« Ìﬂ
            }

            // Seed roles
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                await RoleSeed.SeedRolesAsync(services);
            }




            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI();
            //}



            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
