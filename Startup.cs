using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MyHotelListing.Configurations;
using MyHotelListing.Data;
using MyHotelListing.IRepository;
using MyHotelListing.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyHotelListing
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
			services.AddDbContext<DatabaseContext>(options =>
			  options.UseSqlServer(Configuration.GetConnectionString("sqlConnection"))); //UseSqlServer variations

			services.AddCors(o => {
				o.AddPolicy("AllowAll", builder =>
				builder.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader());
			});

			services.AddAutoMapper(typeof(MapperInitializer));

			services.AddTransient<IUnitOfWork, UnitOfWork>();

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyHotelListing", Version = "v1" });
			});

			services.AddControllers().AddNewtonsoftJson(
				option => option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseSwagger();
			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyHotelListing v1"));

			app.UseHttpsRedirection();

			app.UseCors("AllowAll");

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
			
		}
	}
}
