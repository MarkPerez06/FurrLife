using Microsoft.AspNetCore.SignalR;

namespace FurrLife.Hubs
{
    public class ConsultationHub : Hub
    {
        public async Task SendSignal(string signal, string connectionId)
        {
            if (!string.IsNullOrEmpty(connectionId))
            {
                await Clients.Client(connectionId).SendAsync("ReceiveSignal", signal, Context.ConnectionId);
            }
            else
            {
                await Clients.Others.SendAsync("ReceiveSignal", signal, Context.ConnectionId);
            }
        }

        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
    }
}
