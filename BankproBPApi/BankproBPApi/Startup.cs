using BankproBPData;
using BankproBPData.Core;
using BankproBPDomain.Managers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.Generation.Processors.Security;
using System;
using System.Linq;
using System.Text;

namespace BankproBPApi
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

                  var assembly = System.AppDomain.CurrentDomain.Load("BankproBPDomain");
                  var types = assembly.GetTypes().Where(w => w.IsClass && w.Name.EndsWith("Manager"));
                  var addTransientMethod = typeof(ServiceCollectionServiceExtensions).GetMethods()
                        .FirstOrDefault(s => s.Name == "AddTransient" && s.IsGenericMethod == true && s.GetGenericArguments().Count() == 1);
                  foreach (var type in types)
                  {
                        var method = addTransientMethod.MakeGenericMethod(new[] { type });
                        method.Invoke(services, new[] { services });
                  }

                  services.AddTransient<CompanyProgramManager>();

                  services.AddDbContext<BankproBpDbContext>(options =>
                  {
                        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString"), b => b.MigrationsAssembly("BankproBPApi"));
                  });

                  services.AddIdentity<ApplicationUser, IdentityRole>()
                      .AddEntityFrameworkStores<BankproBpDbContext>()
                      .AddDefaultTokenProviders();

                  services.Configure<IdentityOptions>(options =>
                  {
                        options.Password.RequireUppercase = false;
                        options.Password.RequireNonAlphanumeric = false;
                  });

                  services.AddAuthentication(options =>
                  {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                  }).AddJwtBearer(options =>
                      {
                            options.SaveToken = true;
                            options.RequireHttpsMetadata = false;
                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                  //ClockSkew = TimeSpan.Zero,
                                  ValidateIssuer = true,
                                  ValidateAudience = true,
                                  ValidAudience = Configuration["JWT:ValidAudience"],
                                  ValidIssuer = Configuration["JWT:ValidIssuer"],
                                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                            };
                      });

                  services.AddControllersWithViews().AddNewtonsoftJson(options => {
                        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                  });

                  services.AddHttpContextAccessor();
                  services.AddTransient<CurrentUser>();
                  services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
                  // In production, the Angular files will be served from this directory
                  services.AddSpaStaticFiles(configuration =>
                  {
                        configuration.RootPath = "ClientApp/dist";
                  });

                  services.AddSwaggerDocument(config =>
                  {
                        var apiScheme = new OpenApiSecurityScheme()
                        {
                              Type = OpenApiSecuritySchemeType.ApiKey,
                              Name = "Authorization",
                              In = OpenApiSecurityApiKeyLocation.Header,
                              Description = "Copy this into the value field: Bearer {token}"
                        };

                        config.AddSecurity("JWT Token", Enumerable.Empty<string>(), apiScheme);
                        config.OperationProcessors.Add(
                              new AspNetCoreOperationSecurityScopeProcessor("JWT Token"));

                  });
            }

            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
                  if (env.IsDevelopment())
                  {
                        app.UseDeveloperExceptionPage();
                  }
                  else
                  {
                        app.UseExceptionHandler("/Error");
                        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                        app.UseHsts();
                  }

                  app.UseHttpsRedirection();
                  app.UseStaticFiles();
                  if (!env.IsDevelopment())
                  {
                        app.UseSpaStaticFiles();
                  }

                  app.UseOpenApi();
                  app.UseSwaggerUi3();

                  app.UseRouting();
                  app.UseAuthentication();
                  app.UseAuthorization();

                  app.UseEndpoints(endpoints =>
                  {
                        endpoints.MapControllerRoute(
                      name: "default",
                      pattern: "{controller}/{action=Index}/{id?}");
                  });

                  app.UseSpa(spa =>
                  {
                        // To learn more about options for serving an Angular SPA from ASP.NET Core,
                        // see https://go.microsoft.com/fwlink/?linkid=864501

                        spa.Options.SourcePath = "ClientApp";

                        if (env.IsDevelopment())
                        {
                              spa.UseAngularCliServer(npmScript: "start");
                        }
                  });
            }
      }
}
