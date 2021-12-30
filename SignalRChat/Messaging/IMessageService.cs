using System.Threading.Tasks;

namespace SignalRChat.Messaging
{
    public interface IMessageService
    {
        Task SendEmailAsync(
            MessageOptions message
        );
    }
}
