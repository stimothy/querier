using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Querier.Data;
using Querier.Models;
using Querier.Services;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Text;

namespace Querier
{
    public class Startup
    {
        List<WebSocket> WebSocketList = new List<WebSocket>();
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Tells the application which database to connect to.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();

            //services.AddWebSocketManager();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            
            app.UseWebSockets();
            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/Server/ws")
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                        await UpdateServer(context, webSocket);
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                    }
                }
                if (context.Request.Path == "/Client/ws")
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                        await UpdateClient(context, webSocket);
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                    }
                }
                if (context.Request.Path == "/Both/ws")
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                        WebSocketList.Add(webSocket);
                        await UpdateBoth(context, webSocket);
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                    }
                }
                else
                {
                    await next();
                }

            });
        }

        private async Task UpdateClient(HttpContext context, WebSocket webSocket)
        {
            var buffer = new byte[4 * 1024];

            while (webSocket.State != WebSocketState.Closed)
            {
                string msg = "Hello Client";
                byte[] sendBuffer = Encoding.Unicode.GetBytes(msg);
                await webSocket.SendAsync(sendBuffer, WebSocketMessageType.Text, true, CancellationToken.None);
                System.Threading.Thread.Sleep(1000);
            }
            //WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            //timer.Stop();
            //await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing socket", CancellationToken.None);

            //while (!result.CloseStatus.HasValue)
            //{
            //    await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);

            //    result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            //}
            //await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }

        private async Task UpdateServer(HttpContext context, WebSocket webSocket)
        {
            var buffer = new byte[4 * 1024];

            while (webSocket.State != WebSocketState.Closed)
            {
                string msg = "Hello Server";
                byte[] sendBuffer = Encoding.Unicode.GetBytes(msg);
                await webSocket.SendAsync(sendBuffer, WebSocketMessageType.Text, true, CancellationToken.None);
                System.Threading.Thread.Sleep(1000);
            }
            //WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            //timer.Stop();
            //await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing socket", CancellationToken.None);

            //while (!result.CloseStatus.HasValue)
            //{
            //    await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);

            //    result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            //}
            //await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }

        private async Task UpdateBoth(HttpContext context, WebSocket webSocket)
        {
            var buffer = new byte[4 * 1024];

            while (webSocket.State != WebSocketState.Closed)
            {
                string msg = "Hello Both";
                byte[] sendBuffer = Encoding.Unicode.GetBytes(msg);
                foreach (WebSocket web in WebSocketList)
                {
                    await web.SendAsync(sendBuffer, WebSocketMessageType.Text, true, CancellationToken.None);
                }
                System.Threading.Thread.Sleep(1000);
            }
            //WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            //timer.Stop();
            //await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing socket", CancellationToken.None);

            //while (!result.CloseStatus.HasValue)
            //{
            //    await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);

            //    result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            //}
            //await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }
    }
}
