using CaducaRest.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;
using System;
using CaducaRest.Resources;
using System.Reflection;
using Microsoft.AspNetCore.Localization;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using CaducaRest.Filters;

namespace CaducaRest
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
			services.AddControllers(options =>
			{
				options.Filters.Add(typeof(CustomExceptionFilter));
			})
				 .AddJsonOptions(JsonOptions =>
				 JsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null)
				.AddDataAnnotationsLocalization(options =>
			{
				options.DataAnnotationLocalizerProvider =
				(type, factory) =>
				{
					var assemblyName = new AssemblyName(typeof(SharedResource)
						   .GetTypeInfo().Assembly.FullName);
					return factory.Create("SharedResource", assemblyName.Name);
				};
			});

			services.Configure<RequestLocalizationOptions>(options =>
			{
				var supportedCultures = new List<CultureInfo>
			   {
				   new CultureInfo("es-MX"),
				   new CultureInfo("en-US"),
			   };

				options.DefaultRequestCulture =
				   new RequestCulture(culture: "es-MX", uiCulture: "es-MX");
				options.SupportedCultures = supportedCultures;
				options.SupportedUICultures = supportedCultures;

				options.RequestCultureProviders
				   .Insert(0, new QueryStringRequestCultureProvider());
			});

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Caduca REST", Version = "v1" });
				//Obtenemos el directorio actual
				var basePath = AppContext.BaseDirectory;
				//Obtenemos el nombre de la dll por medio de reflexión
				var assemblyName = System.Reflection.Assembly
							  .GetEntryAssembly().GetName().Name;
				//Al nombre del assembly le agregamos la extensión xml
				var fileName = System.IO.Path
							  .GetFileName(assemblyName + ".xml");
				//Agregamos el Path, es importante utilizar el comando
				// Path.Combine ya que entre windows y linux 
				// rutas de los archivos
				// En windows es por ejemplo c:/Umostarsuarios con / 
				// y en linux es \usr con \
				var xmlPath = Path.Combine(basePath, fileName);
				c.IncludeXmlComments(xmlPath);
			});
			services.AddDbContext<CaducaContext>(options =>
				  options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddSingleton<LocService>();
			services.AddLocalization(options => options.ResourcesPath = "Resources");
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			var locOptions = app.ApplicationServices
						 .GetService<IOptions<RequestLocalizationOptions>>();
			app.UseRequestLocalization(locOptions.Value);

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c =>
				{
					c.SwaggerEndpoint("/swagger/v1/swagger.json", "CaducaRest v1");
					c.DefaultModelsExpandDepth(-1);
				});
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
