using Microsoft.AspNetCore.SignalR;
using Tune.site.Components;
using DBL;
using System.Threading.Tasks;

namespace Tune.site
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await Resend.message(0);
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            // Register MessagesDB for dependency injection
            builder.Services.AddScoped<MessagesDB>();

            var app = builder.Build();

            // Server Side Hub for signalR
            app.MapHub<Tune.site.Hubs.ChatHub>("/chatHub");

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}