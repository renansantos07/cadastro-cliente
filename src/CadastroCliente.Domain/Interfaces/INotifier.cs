using CadastroCliente.Domain.Notifications;

namespace CadastroCliente.Domain.Interfaces
{
    public interface INotifier
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void Handle(string notification);
        void Handle(Notification notification);
    }
}