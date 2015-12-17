using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WPF.Toaster
{

    public partial class Toasts
    {
        private const byte MaxNotifications = 4;
        private int _count;
        public ToastsCollection ToastsCollection = new ToastsCollection();
        private readonly ToastsCollection _buffer = new ToastsCollection();

        public Toasts(NotificationLocation location = NotificationLocation.BottonRight)
        {
            InitializeComponent();
            NotificationsControl.DataContext = ToastsCollection;
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

        public void AddNotification(Toast toast)
        {
            toast.Id = _count++;
            if (ToastsCollection.Count + 1 > MaxNotifications)
            {
                _buffer.Add(toast);
            }
            else
            {
                ToastsCollection.Add(toast);
            }

            //Show window if there're notifications
            if (ToastsCollection.Count > 0 && !IsActive)
            {
                Show();
            }
        }

        public void RemoveNotification(Toast toast)
        {
            if (ToastsCollection.Contains(toast))
            {
                ToastsCollection.Remove(toast);
            }

            if (_buffer.Count > 0)
            {
                ToastsCollection.Add(_buffer[0]);
                _buffer.RemoveAt(0);
            }

            //Close window if there's nothing to show
            if (ToastsCollection.Count < 1)
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
            RemoveNotification(ToastsCollection.First(n => n.Id == int.Parse(element.Tag.ToString())));
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
