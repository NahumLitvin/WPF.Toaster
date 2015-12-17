using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using JetBrains.Annotations;

namespace WPFGrowlNotification
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public partial class GrowlNotifications
    {
        private const byte MaxNotifications = 4;
        private int _count;
        public Notifications Notifications = new Notifications();
        private readonly Notifications _buffer = new Notifications();

        public GrowlNotifications(NotificationLocation location = NotificationLocation.BottonRight)
        {
            InitializeComponent();
            NotificationsControl.DataContext = Notifications;
            SetNotificationsLocation(location);
        }

        public void SetNotificationsLocation(NotificationLocation location)
        {
            switch (location)
            {
                case NotificationLocation.TopRight:
                    Top = 0;
                    Left = SystemParameters.PrimaryScreenWidth - Width;
                    NotificationsControl.VerticalAlignment = VerticalAlignment.Top;
                    break;
                case NotificationLocation.BottonRight:
                    Top = SystemParameters.PrimaryScreenHeight - Height;
                    Left = SystemParameters.PrimaryScreenWidth - Width;
                    NotificationsControl.VerticalAlignment = VerticalAlignment.Bottom;
                    break;
                case NotificationLocation.TopLeft:
                    Top = 0;
                    Left = 0;
                    NotificationsControl.VerticalAlignment = VerticalAlignment.Top;
                    break;
                case NotificationLocation.ButtomLeft:
                    Top = SystemParameters.PrimaryScreenHeight - Height;
                    Left = 0;
                    NotificationsControl.VerticalAlignment = VerticalAlignment.Bottom;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("location", location, null);
            }
        }

        public void AddNotification(Notification notification)
        {
            notification.Id = _count++;
            if (Notifications.Count + 1 > MaxNotifications)
            {
                _buffer.Add(notification);
            }
            else
            {
                Notifications.Add(notification);
            }

            //Show window if there're notifications
            if (Notifications.Count > 0 && !IsActive)
            {
                Show();
            }
        }

        public void RemoveNotification(Notification notification)
        {
            if (Notifications.Contains(notification))
            {
                Notifications.Remove(notification);
            }

            if (_buffer.Count > 0)
            {
                Notifications.Add(_buffer[0]);
                _buffer.RemoveAt(0);
            }

            //Close window if there's nothing to show
            if (Notifications.Count < 1)
            {
                Hide();
            }
        }

        private void NotificationWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (Math.Abs(e.NewSize.Height) > 1)
            {
                return;
            }
            var element = sender as Grid;
            RemoveNotification(Notifications.First(n => n.Id == int.Parse(element.Tag.ToString())));
        }
    }

    public enum NotificationLocation
    {
        TopRight,
        BottonRight,
        TopLeft,
        ButtomLeft
    }
}
