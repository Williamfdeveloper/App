using App.Adapters;
using App.Domain;
using App.Domain.Contracts;
using App.Domain.Contracts.Adapter;
using App.Domain.Contracts.Repository;
using App.Domain.Entities;
using App.Domain.Service;
using App.Repository.Context;
using App.Repository.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.UI
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
            services.AddRazorPages();

            services.AddDbContext<DefaultContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SqlServerConnection")));
            services.AddIdentity<Usuario, IdentityRole>().AddEntityFrameworkStores<DefaultContext>();

            #region IoC
            //Domain
            //services.AddTransient<IAccountService, AccountService>();
            //services.AddTransient<IProdutoService, ProdutoService>();
            //services.AddTransient<IDadosCartaoService, DadosCartaoService>();
            //services.AddTransient<ITokenService, TokenService>();
            //services.AddTransient<ILoggerService, LoggerService>();
            //services.AddTransient<IFormasPagamentoService, FormasPagamentoService>();
            //services.AddTransient<IEmailSistemaService, EmailSistemaService>();
            //services.AddTransient<IParametrosService, ParametrosService>();
            //services.AddTransient<IPedidoService, PedidoService>();
            //services.AddTransient<IPagamentoService, PagamentoService>();

            //services.AddTransient<IMessageQueueService, MessageQueueService>();
            //services.AddTransient<IHostedService, SchedulerService>();
            services.AddTransient<IAccountApiService, AccountApiService>();
            services.AddTransient<IProdutoApiService, ProdutoApiService>();


            //Adapter
            services.AddTransient<IAccountAdapter, AccountAdapter>();
            services.AddTransient<IProdutoAdapter, ProdutoAdapter>();
            services.AddTransient<IPedidoAdapter, PedidoAdapter>();
            //services.AddTransient<IPagamentoAdapter, PagamentoAdapter>();

            //Repository
            //services.AddTransient<IDadosCartaoRepository, DadosCartaoRepository>();
            //services.AddTransient<IEmailSistemaRepository, EmailSistemaRepository>();
            //services.AddTransient<IProdutoRepository, ProdutoRepository>();
            //services.AddTransient<IParametrosRepository, ParametrosRepository>();
            //services.AddTransient<ILoggerRepository, LoggerRepository>();
            //services.AddTransient<IPedidoRepository, PedidoRepository>();
            //services.AddTransient<IFormasPagamentoRepository, FormasPagamentoRepository>();

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            #endregion


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

            //services.AddIdentityCore<Usuario>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<DefaultContext>()
            //    .AddTokenProvider<DataProtectorTokenProvider<Usuario>>(TokenOptions.DefaultProvider);

            services.AddMvc().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
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

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

            

        }
    }
}
