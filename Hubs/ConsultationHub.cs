using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace FurrLife.Hubs
{
    public class ConsultationHub : Hub
    {
        // Dictionary to track consultation IDs and their associated connection IDs
        private static readonly ConcurrentDictionary<string, ConcurrentBag<string>> ConsultationGroups = new ConcurrentDictionary<string, ConcurrentBag<string>>();

        public async Task JoinConsultation(string consultationId)
        {
            // Add the connection ID to the group for the given consultation ID
            var connectionId = Context.ConnectionId;
            if (!ConsultationGroups.ContainsKey(consultationId))
            {
                ConsultationGroups[consultationId] = new ConcurrentBag<string>();
            }

            ConsultationGroups[consultationId].Add(connectionId);

            // Notify others in the group that this connection has joined
            await Groups.AddToGroupAsync(connectionId, consultationId);
        }

        public async Task SendSignal(string signal, string consultationId)
        {
            if (ConsultationGroups.ContainsKey(consultationId))
            {
                // Broadcast the signal to all clients in the consultation group except the sender
                await Clients.GroupExcept(consultationId, Context.ConnectionId)
                             .SendAsync("ReceiveSignal", signal, Context.ConnectionId, consultationId);
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            // Remove the connection ID from all consultation groups it was part of
            foreach (var group in ConsultationGroups)
            {
                if (group.Value.Contains(Context.ConnectionId))
                {
                    group.Value.TryTake(out _);
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, group.Key);

                    // Remove empty groups
                    if (group.Value.IsEmpty)
                    {
                        ConsultationGroups.TryRemove(group.Key, out _);
                    }
                }
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}


//using Microsoft.AspNetCore.SignalR;

//namespace FurrLife.Hubs
//{
//    public class ConsultationHub : Hub
//    {
//        public async Task SendSignal(string signal, string connectionId)
//        {
//            if (!string.IsNullOrEmpty(connectionId))
//            {
//                await Clients.Client(connectionId).SendAsync("ReceiveSignal", signal, Context.ConnectionId);
//            }
//            else
//            {
//                await Clients.Others.SendAsync("ReceiveSignal", signal, Context.ConnectionId);
//            }
//        }

//        public string GetConnectionId()
//        {
//            return Context.ConnectionId;
//        }
//    }
//}
