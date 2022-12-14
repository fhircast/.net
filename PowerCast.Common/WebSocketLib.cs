using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.IO;

namespace Nuance.PowerCast.Common
{
	public class WebSocketLib : IWebSocketLib
	{
		public async Task SendStringAsync(WebSocket socket, string data, CancellationToken ct = default(CancellationToken))
		{
			var buffer = Encoding.UTF8.GetBytes(data);
			var segment = new ArraySegment<byte>(buffer);
			await socket.SendAsync(segment, WebSocketMessageType.Text, true, ct);
		}

		public async Task<string> ReceiveStringAsync(WebSocket socket, CancellationToken ct = default(CancellationToken))
		{
			var buffer = new ArraySegment<byte>(new byte[8192]);
			using (var ms = new MemoryStream())
			{
				WebSocketReceiveResult result;
				do
				{
					ct.ThrowIfCancellationRequested();

					result = await socket.ReceiveAsync(buffer, ct);
					ms.Write(buffer.Array, buffer.Offset, result.Count);
				}
				while (!result.EndOfMessage);

				ms.Seek(0, SeekOrigin.Begin);
				if (result.MessageType != WebSocketMessageType.Text)
				{
					return null;
				}
				using (var reader = new StreamReader(ms, Encoding.UTF8))
				{
					string data = await reader.ReadToEndAsync();
					System.Diagnostics.Debug.WriteLine($"received data: {data}");
					return data;
				}
			}
		}
	}
}
