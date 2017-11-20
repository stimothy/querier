using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;

namespace Querier.Controllers
{
    public class SocketController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Server()
        {
            return View();
        }

        [NonAction]
        public async void Server(HttpContext context)
        {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    if (webSocket != null && webSocket.State == WebSocketState.Open)
                    {
                        while (webSocket.State != WebSocketState.Closed)
                        {
                            string msg = "Hi from Server controller.";
                            byte[] sendBuffer = Encoding.Unicode.GetBytes(msg);
                            await webSocket.SendAsync(sendBuffer, WebSocketMessageType.Text, true, CancellationToken.None);
                            System.Threading.Thread.Sleep(1000);
                        }
                    }
                }
        }

        //[HttpGet("v1/resources/{id}")]
        /*public async Task<IActionResult> Server(string id)
        {
            /*var resource = await this.repository.GetAsync(id);
            if (resource == null)
            {
                return new HttpStatusCodeResult(404);
            }
            *//*
            if (this.HttpContext.Request.Path == "/Socket/Server")
            if (this.HttpContext.WebSockets.IsWebSocketRequest)
            {
                var webSocket = await this.HttpContext.WebSockets.AcceptWebSocketAsync();
                if (webSocket != null && webSocket.State == WebSocketState.Open)
                {
                    while (webSocket.State != WebSocketState.Closed)
                    {
                        string msg = "Server";
                        byte[] sendBuffer = Encoding.Unicode.GetBytes(msg);
                        await webSocket.SendAsync(sendBuffer, WebSocketMessageType.Text, true, CancellationToken.None);
                        System.Threading.Thread.Sleep(1000);
                    }
                }
            }

            return View();
        }*/
    }
}