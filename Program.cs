using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;
using MovieAPI_Project.Data;

namespace MovieAPI_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<MovieAPI_ProjectContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MovieAPI_ProjectContext") ?? throw new InvalidOperationException("Connection string 'MovieAPI_ProjectContext' not found.")));

            //Dependency for connection- invoking the connection string
            builder.Services.AddDbContext<MovieDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
            });
            //add dependency auto mapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Movie API Documentation",
                    Description = "REST API which allows user to manipulate Movie Characters",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Riya Raj Kanojia",
                        Email = "riya9911396583@gmail.com",
                        Url = new Uri("https://google.com"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "License",
                        Url = new Uri("https://google.com")
                    }
                });
                //using System.Reflection;
                // Set the comments path for the Swagger JSON and UI.
                //var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
               
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}