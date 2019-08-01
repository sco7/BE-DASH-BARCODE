using System.Text;
using FontaineVerificationProject.Models;
using FontaineVerificationProject.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using System;
using FontaineVerificationProjectBack.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Neodynamic.SDK.Printing;

namespace FontaineVerificationProject
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }
        private static readonly SymmetricSecurityKey SigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("Chatburn01200441977"));

        // This method gets called by the runtime. Use this method to add services to the container.
        [System.Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<FontaineContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SQLConnection")));
            services.Configure<JwtIssuerOptions>(options => Configuration.GetSection("JwtIssuerOptions").Bind(options));
            services.Configure<KestrelConfiguration>(options => Configuration.GetSection("KestrelOptions").Bind(options));
            services.Configure<PrintingConfig>(options => Configuration.GetSection("Printing").Bind(options));

            var sp = services.BuildServiceProvider();

            KestrelConfiguration kestrelConfig = sp.GetService<IOptions<KestrelConfiguration>>().Value;
            JwtIssuerOptions jwtAppSettingOptions = sp.GetService<IOptions<JwtIssuerOptions>>().Value;

            ThermalLabel.LicenseKey = "TC6KYFLVC9VUP72M43LMGHGW592EP2W3Q8FF2BRMM3X9JFZHHYXQ";
            ThermalLabel.LicenseOwner = "Dash Computer Products-Ultimate Edition-OEM Developer License";

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    //options.TokenValidationParameters = new TokenValidationParameters
                    //{
                    //    ValidateIssuerSigningKey = true,
                    //    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                    //        .GetBytes("Secure")),
                    //    ValidateIssuer = false,
                    //    ValidateAudience = false
                    //};
                    options.RequireHttpsMetadata |= kestrelConfig.SslSettings != null;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtAppSettingOptions.Issuer,

                        ValidateAudience = true,
                        ValidAudience = jwtAppSettingOptions.Audience,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SigningKey,

                        RequireExpirationTime = true,
                        ValidateLifetime = true,

                        ClockSkew = TimeSpan.Zero
                    };
                });
            services.AddOptions();

            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions.Issuer;
                options.Audience = jwtAppSettingOptions.Audience;
                options.SigningCredentials = new SigningCredentials(SigningKey, SecurityAlgorithms.HmacSha256);
            });

            services.AddCors(options =>
            {
                if (kestrelConfig.Cors != null)
                {
                    options.AddPolicy("Allow", builder =>
                    {
                        builder
                       .AllowCredentials()
                       .WithHeaders(kestrelConfig.Cors.AllowedHeaders.Split(","))
                       .WithMethods(kestrelConfig.Cors.AllowedMethods.Split(","))
                       .WithOrigins(kestrelConfig.Cors.AllowedOrigins.Split(","))
                       .WithExposedHeaders("Location", "location");
                    });
                }
                else
                {
                    options.AddPolicy("Allow", builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials()
                           .WithExposedHeaders("Location");
                    });
                }
            });

            services.AddAutoMapper();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            var serverConfig = serviceProvider.GetService<IOptions<KestrelConfiguration>>().Value;

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseHsts();
            //}


            app.UseCors("Allow");

            app.UseAuthentication();

            app.Use(async (context, next) =>
            {
                try
                {
                    await next();
                }
                catch (Exception e)
                {
                    if (context.Response.HasStarted)
                    {
                        throw;
                    }
                    var errorsToReturn = new
                    {
                        e.Message,
                        e.StackTrace,
                        e.HelpLink
                    };
                    context.Response.Clear();
                    AddCorsHeaders(context, serverConfig);
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(
                    errorsToReturn));
                    return;
                }
            });

            //app.UseHttpsRedirection();
            app.UseMvc();

             
        }

        private void AddCorsHeaders(HttpContext context, KestrelConfiguration config)
        {
            context.Response.Headers.Add("Access-Control-Allow-Origin", config.Cors.AllowedOrigins);
            context.Response.Headers.Add("Access-Control-Allow-Headers", config.Cors.AllowedHeaders);
            context.Response.Headers.Add("Access-Control-Allow-Methods", config.Cors.AllowedMethods);
        }
    }
}
