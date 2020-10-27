using DevIO.Bussiness.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevIO.Bussiness.Notifications
{
    public class Notify : INotify
    {
        private List<Notification> _notifications;

        public Notify()
        {
            _notifications = new List<Notification>();
        }
        public List<Notification> GetNotifications()
        {
            return _notifications;
        }

        public void Handle(Notification notification)
        {
            _notifications.Add(notification);
        }


        public bool HasNotification()
        {
            return _notifications.Any();
        }

      
    }
}
