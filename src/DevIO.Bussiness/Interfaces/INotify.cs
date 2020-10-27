using DevIO.Bussiness.Notifications;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace DevIO.Bussiness.Interfaces
{
    public interface INotify
    {
        bool HasNotification();

        List<Notification> GetNotifications();

        void Handle(Notification notification);
    }
}
