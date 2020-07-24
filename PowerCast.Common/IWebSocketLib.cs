using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Nuance.PowerCast.Common
{
	public interface IWebSocketLib
	{
		Task<string> ReceiveStringAsync(WebSocket socket, CancellationToken ct = default);
		Task SendStringAsync(WebSocket socket, string data, CancellationToken ct = default);
	}
}