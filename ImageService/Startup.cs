using ImageService.IService;
using ImageService.Model;
using ImageService.Service;
using Microsoft.OpenApi.Models;

namespace ImageService
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
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			services.AddCors(options =>
			{
				options.AddPolicy(name: "AllowSpecificOrigin", builder =>
				{
					builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
				});
			});

			services.AddControllers();
			services.AddTransient<ICreateUser<MCreateUser>, CreateUserService>();
			services.AddTransient<IUploadFile, ImageUPService>();

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Imager Service", Version = "1.0.0" });

			});


		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c =>
				{
					c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
					c.DisplayRequestDuration();
				});
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseCors("AllowSpecificOrigin");
			// app.UseAuthentication();
			app.UseAuthorization();
			app.UseStaticFiles();// For the wwwroot folder

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
