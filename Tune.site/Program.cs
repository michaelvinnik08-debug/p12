using Microsoft.AspNetCore.SignalR;
using Tune.site.Components;
using DBL;
using Resend;

namespace Tune.site
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddHttpClient();

            // Register Resend
            builder.Services.AddOptions();
            builder.Services.AddHttpClient<ResendClient>();
            builder.Services.Configure<ResendClientOptions>(o =>
            {
                o.ApiToken = "YOUR_NEW_API_KEY_HERE"; // use your new key after regenerating!
            });
            builder.Services.AddTransient<IResend, ResendClient>();

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