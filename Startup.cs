using DataModels;
using DataModels.MZReports;
using DataModels.OraDB;
using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using LinqToDB.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MizeApi.Helper;
using MizeApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace MizeApi
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
            services.AddTransient<IAttendanceService, AttendanceService>();
            services.AddTransient<IMzscService, MzscService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddScoped<IEmailSender,EmailSender>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddLinqToDBContext<MizePortalDB>((provider, options) =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                .UseDefaultLogging(provider);
            });
            services.AddLinqToDBContext<DataModels.OraDbMsc.OraDbMscDB>((provider, options) =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("OraMscConnection"))
                .UseDefaultLogging(provider);
            });
            services.AddLinqToDBContext<MizeCRMDB>((provider, options) =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("MizeCrMConnection"))
                .UseDefaultLogging(provider);
            });
            services.AddLinqToDBContext<OraDbDB>((provider, options) =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("OraDbConnection"))
                .UseDefaultLogging(provider);
            });
            services.AddLinqToDBContext<MZReportsDB>((provider, options) =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("MZReportsConnection"))
                .UseDefaultLogging(provider);
            });

            ServicePointManager.ServerCertificateValidationCallback =
        delegate (
            object s,
            X509Certificate certificate,
            X509Chain chain,
            SslPolicyErrors sslPolicyErrors
        )
        {
            return true;
        };


            services.AddCors(options =>
            {
                options.AddPolicy(name: "MyCorsPolicy",
                                  policy =>
                                  {
                                      //policy.WithOrigins("http://localhost:5179", "https://crm.mize.sa","http://crm.mize.sa").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                                      policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                                  });
            });
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Demo API",
                    Version = "v1",
                    Description = "Demo services",
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo services"));
            }
            app.UseCors("MyCorsPolicy");
            //app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
