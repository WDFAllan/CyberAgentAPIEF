using System.Threading.Tasks;

namespace CyberAgentWebAPI.Hubs.Clients
{
    public interface IChatClient
    {
        Task ReceiveMessage(string user, string message);
    }
}
