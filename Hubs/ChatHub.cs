using Microsoft.AspNetCore.SignalR;

namespace FurrLife.Hubs
{
    using Microsoft.AspNetCore.SignalR;

    public class ChatHub : Hub
    {
        public async Task NotifyRefreshChat()
        {
            await Clients.All.SendAsync("RefreshChat");
        }
    }
}
