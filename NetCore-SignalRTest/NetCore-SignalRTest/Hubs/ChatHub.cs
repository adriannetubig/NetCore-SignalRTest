using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace NetCore_SignalRTest.Hubs
{
    public class ChatHub: Hub
    {
        private const string _group = "Team Ba";
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendToSelf(string user, string message)
        {
            if (message.Contains("Group"))
                await Groups.AddToGroupAsync(Context.ConnectionId, _group);

            await Clients.Caller.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendToAllButSelf(string user, string message)
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendToGroup(string user, string message)
        {
            await Clients.Group(_group).SendAsync("ReceiveMessage", user, message);
        }
    }
}
