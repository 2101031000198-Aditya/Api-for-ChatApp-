using Microsoft.AspNet.SignalR;

namespace chatapp.Hubs
{
    public class ChatHub : Hub
    {
        public void SendMessage(string user, string message)
        {
            // Call the client-side method 'ReceiveMessage'
            Clients.All.ReceiveMessage(user, message);
        }
    }
}
