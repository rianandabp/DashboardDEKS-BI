using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DashboardDeks.Data;
using DashboardDeks.Services.Document;
using DashboardDeks.Services.Notification;
using DashboardDeks.Services.Outline;
using DashboardDeks.Services.Partner;
using DashboardDeks.Services.Program;
using DashboardDeks.Services.Task;
using DashboardDeks.Services.User;
using DashboardDeks.Services.PartnerReport;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using DashboardDeks.Services.Status;

namespace MonitorDeks.Web
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
            services.AddCors();

            services.AddControllers();
            services.AddDbContext<ProjectDbContext>(opts => {
                opts.EnableDetailedErrors();
                opts.UseNpgsql(Configuration.GetConnectionString("dashboard.deks"));
            });

            services.AddTransient<IDocumentService, DocumentService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IOutlineService, OutlineService>();
            services.AddTransient<IProgramService, ProgramService>();
            services.AddTransient<ITaskService, TaskService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPartnerService, PartnerService>();
            services.AddTransient<IPartnerReportService, PartnerReportService>();
            services.AddTransient<IStatusService, StatusService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(
                builder => builder
                    .WithOrigins(
                        "http://localhost:8080",
                        "http://localhost:8081",
                        "http://localhost:8082")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                );

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
