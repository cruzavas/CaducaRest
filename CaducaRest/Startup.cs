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

			services.AddControllers();
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
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "CaducaRest v1");
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
