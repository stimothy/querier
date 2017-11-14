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

        public async Task Update(HttpContext context, WebSocket webSocket)
        {
            var buffer = new byte[4 * 1024];

            while (webSocket.State != WebSocketState.Closed)
            {
                string msg = "Hello";
                byte[] sendBuffer = Encoding.Unicode.GetBytes(msg);
                await webSocket.SendAsync(sendBuffer, WebSocketMessageType.Text, true, CancellationToken.None);
                System.Threading.Thread.Sleep(1000);
            }
            /*
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            timer.Stop();
            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing socket", CancellationToken.None);

            while (!result.CloseStatus.HasValue)
            {
                await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
            */
        }
    }
}