﻿using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using CarRentalDDD.API.Mappings;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.EntityFrameworkCore;
using CarRentalDDD.Infra.Repositories;
using CarRentalDDD.Domain.Models.Customers;
using CarRentalDDD.Infra.Customers.Repositories;
using CarRentalDDD.Domain.SeedWork;
using CarRentalDDD.Infra.Cars.Repositories;
using CarRentalDDD.Domain.Models.Cars;
using CarRentalDDD.Infra.Rentals.Repositories;
using CarRentalDDD.Domain.Models.Rentals;
using CarRentalDDD.Infra.Emails;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CarRentalDDD.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RentalContext>(opt => opt.UseInMemoryDatabase("memorydb"));

            services.AddMediatR(typeof(Startup));

            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(typeof(Mapping));

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();       
            
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
