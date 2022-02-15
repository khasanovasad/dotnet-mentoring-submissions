using System;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Email;

namespace BrainstormSessions
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File($"logs/brain-storm-sessions.log", rollingInterval: RollingInterval.Hour)
                .WriteTo.Email(new EmailConnectionInfo
                {
                    FromEmail = "test1@test.com",
                    ToEmail = "test1@test.com",
                    MailServer = "smtp.yandex.com",
                    EmailSubject = "BrainstormSession errors"
                }, restrictedToMinimumLevel: LogEventLevel.Error, batchPostingLimit: 1) 
                .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog();
    }
}
