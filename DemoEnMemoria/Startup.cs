using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore.InMemory;
//using Microsoft.EntityFrameworkCore;
using DemoEnMemoria.Modelo;
using Microsoft.EntityFrameworkCore;

namespace DemoEnMemoria
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EmpleadoDbContext>
                (option => option.UseInMemoryDatabase(Configuration.GetConnectionString("MyDb")));
            //(optionsAction: option:DbContextOptionsBuilder => option.UseInMemoryDataBase());
                //(option => option.UseSqlServer(Configuration("database:connection")));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DemoEnMemoria", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DemoEnMemoria v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<EmpleadoDbContext>();
            DatosIniciales(context);

        }
        public static void DatosIniciales(EmpleadoDbContext context)
        {
            Empleados emp1 = new Empleados
            {
                Id = 2,
                nombre = "Claudia Rivera",
                genero = "Femenino",
                salario = 4500000,
                edad = 37
            };

            Empleados emp2 = new Empleados
            {
                Id = 3,
                nombre = "Sandra Milena Navas",
                genero = "Femenino",
                salario = 4100000,
                edad = 39
            };

            context.Empleados.Add(emp1);
            context.Empleados.Add(emp2);
            context.SaveChanges();
        }

    }
}
