using App.Domain;
using App.Domain.Contracts;
using App.Domain.Contracts.Repository;
using App.Domain.Entities;
using App.Domain.Service;
using App.Repository.Context;
using App.Repository.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Api
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
            services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
                .AddAzureADBearer(options => Configuration.Bind("AzureAd", options));
            services.AddControllers();


            services.AddDbContext<DefaultContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlServerConnection")));
            services.AddIdentity<Usuario, IdentityRole>().AddEntityFrameworkStores<DefaultContext>();


            #region IoC
            //Domain
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IProdutoService, ProdutoService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IEmailSistemaService, EmailSistemaService>();
            services.AddTransient<IParametrosService, ParametrosService>();

            //Adapter
            //services.AddSingleton<IUsuarioAdapter, UsuarioAdapter>();

            //Repository
            services.AddTransient<IEmailSistemaRepository, EmailSistemaRepository>();
            services.AddTransient<IProdutoRepository, ProdutoRepository>();
            services.AddTransient<IParametrosRepository, ParametrosRepository>();

            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            #endregion

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "William F Silva", Version = "v1", });
            });

            var key = Encoding.ASCII.GetBytes(Constant.SecretJwt);

            services.AddAuthentication()
            .AddCookie(options =>
            {
                options.Events.OnRedirectToLogin = options.Events.OnRedirectToAccessDenied = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                };
            })
            .AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    //ValidIssuer = Configuration["JwtIssuer"],
                    //ValidAudience = Configuration["JwtIssuer"],
                    //IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero // remove delay of token when expire
                };
            });

            services.AddIdentityCore<Usuario>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<DefaultContext>()
                .AddTokenProvider<DataProtectorTokenProvider<Usuario>>(TokenOptions.DefaultProvider);

            services.AddMvc().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider, UserManager<Usuario> userManager)
        {

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<DefaultContext>().Database.Migrate();
            }

            CreateRoles(serviceProvider).Wait();
            SeedUsers(userManager);
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<Usuario>>();
            string[] rolesNames = { "Admin", "User", "User_Premium" };
            IdentityResult result;
            foreach (var namesRole in rolesNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(namesRole);
                if (!roleExist)
                {
                    result = await roleManager.CreateAsync(new IdentityRole(namesRole));
                }
            }
        }

        private static void SeedUsers(UserManager<Usuario> userManager)
        {
            if (userManager.FindByNameAsync("Williamf.developer@gmail.com").Result == null)
            {
                var user = new Usuario
                {
                    Id = "44276c41-f2a1-40f6-a9ce-ec0638046200",
                    UserName = "Williamf.developer@gmail.com",
                    Email = "Williamf.developer@gmail.com",
                    NormalizedUserName = "WILL",
                    NormalizedEmail = "WILLIAMF.DEVELOPER@GMAIL.COM",
                    EmailConfirmed = true,

                    CPF = "33167387890",
                    Nome = "William Fernando da Silva",
                    DataNascimento = Convert.ToDateTime("22/02/1986"),
                    Sexo = (int)EnumTipo.Sexo.Masculino,
                    Endereco = new Endereco()
                    {
                        Idusuario = "44276c41-f2a1-40f6-a9ce-ec0638046200",
                        CEP = "13056116",
                        Rua = "Antonio Rosique Garcia",
                        Numero = "74",
                        Bairro = "Jd Aeronave de Viracopos",
                        Cidade = "Campinas",
                        Estado = "SP"
                    }
                };

                var password = "BR@sil500";

                var result = userManager.CreateAsync(user, password).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                    userManager.AddToRoleAsync(user, "User").Wait();
                    //userManager.AddToRoleAsync(user, "User_Premium").Wait();
                }
            }
        }
    }
}
