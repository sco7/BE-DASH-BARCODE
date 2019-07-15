using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FontaineVerificationProjectBack.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FontaineVerificationProject
{
    public class Program
    {
        //public static void Main(string[] args)
        //{
        //    CreateWebHostBuilder(args).Build().Run();
        //}

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //        .UseKestrel(opts =>
        //        {
        //            opts.ListenAnyIP(8081);
        //        })
        //        .UseContentRoot(Directory.GetCurrentDirectory())
        //        .UseIISIntegration()
        //        .UseStartup<Startup>();

        public static IConfiguration Configuration { get; set; }

        public static void Main(string[] args)
        {
            Console.Title = "Dexterity Access Management API";
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            try
            {
                var settings = new KestrelConfiguration();
                Configuration.GetSection("KestrelOptions").Bind(settings);
                Console.WriteLine(settings.Cors.AllowedHeaders);
                Console.WriteLine(settings.Cors.AllowedMethods);
                Console.WriteLine(settings.Cors.AllowedOrigins);
                var host = new WebHostBuilder()
                    .UseKestrel(options =>
                    {
                        if (settings.SslSettings != null)
                        {
                            options.Listen(IPAddress.Any, settings.Port, listenOptions =>
                            {
                                listenOptions.UseHttps(settings.SslSettings.CertificatePath, settings.SslSettings.CertificatePassword);
                            });
                        }
                        else
                        {
                            options.Listen(IPAddress.Any, settings.Port);
                        }
                    })
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseIISIntegration()
                    .UseStartup<Startup>()
                    .Build();

                host.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
