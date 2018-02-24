using GP.UserService.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GP.NotificationService.Domain
{
    public interface INotificationActor
    {
        Task SendConfirmationEmailAsync(User user);
        Task SendChangePasswordEmailAsync(User user);
    }
}
