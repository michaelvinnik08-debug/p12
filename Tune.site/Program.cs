using Microsoft.AspNetCore.SignalR;
using Tune.site.Components;
using DBL; 

namespace Tune.site
{
    public class Program
    {
        public static void Main(string[] args)
        {
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