using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace FoodDlvProject2.Hubs
{
    public class ChatHub : Hub
    {
        public async Task Login(string name)
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync
                ("online", $"{name}進入聊天室");
        }
        public async Task SignOut(string name)
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync
                ("online", $"{name}離開聊天室");
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        public async Task SendMessageByServer(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, "系统通知:" + message);
        }

    }
}
