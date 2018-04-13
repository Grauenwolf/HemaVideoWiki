using HemaVideoLib.Services;
using HemaVideoWiki.Data;
using HemaVideoWiki.Models;
using HemaVideoWiki.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Tortuga.Chain;
using Tortuga.Chain.AuditRules;

namespace HemaVideoWiki
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
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddIdentity<ApplicationUser, IdentityRole>(options =>
			{
				// Password settings (setting really low since we don't hold sensitve information)
				options.Password.RequireDigit = false;
				options.Password.RequiredLength = 6;
				options.Password.RequiredUniqueChars = 1;
				options.Password.RequireLowercase = false;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
			}).AddEntityFrameworkStores<ApplicationDbContext>()
			  .AddDefaultTokenProviders();

			// Add application services.
			services.AddTransient<IEmailSender, EmailSender>();

			services.AddMvc();

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
			});

			services.AddSingleton(
				new SqlServerDataSource(Configuration.GetConnectionString("DefaultConnection"))
				.WithRules(
					new UserDataRule("CreatedByUserKey", "UserKey", OperationTypes.Insert),
					new UserDataRule("ModifiedByUserKey", "UserKey", OperationTypes.InsertOrUpdate)
					)
				);
			services.AddSingleton<BookService>();
			services.AddSingleton<PlayService>();
			services.AddSingleton<TagsService>();
			services.AddSingleton<VideoService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseBrowserLink();
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();

			app.UseAuthentication();

			app.UseSwagger();

			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
			});

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}