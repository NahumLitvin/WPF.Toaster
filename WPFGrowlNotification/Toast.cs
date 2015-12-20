using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using JetBrains.Annotations;

namespace WPF.Toaster
{
    /// <summary>
    /// A single toast notification
    /// </summary>
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class Toast : INotifyPropertyChanged
    {
        public Toast()
        {
            TitleColor = Colors.White;
            SubtitleColor = Colors.CornflowerBlue;
            TextColor = Colors.White;
            BackgroundColor = Color.FromRgb(0x2a, 0x33, 0x45);  
        }
        private string _message;
        public string Message
        {
            get { return _message; }

            set
            {
                if (_message == value) return;
                _message = value;
                OnPropertyChanged("Message");
            }
        }

        private int _id;
        public int Id
        {
            get { return _id; }

            set
            {
                if (_id == value) return;
                _id = value;
                OnPropertyChanged("Id");
            }
        }

        private BitmapImage _image;
        public BitmapImage Image
        {
            get { return _image; }

            set
            {
                if (Equals(_image, value)) return;
                _image = value;
                OnPropertyChanged("Image");
            }
        }

        private string _title;
        public string Title
        {
            get { return _title; }

            set
            {
                if (_title == value) return;
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        private string _subTitle;
        public string SubTitle
        {
            get { return _subTitle; }

            set
            {
                if (_subTitle == value) return;
                _subTitle = value;
                OnPropertyChanged("SubTitle");
            }
        }

        public Color BackgroundColor { get; set; }

        public Color TitleColor { get; set; }

        public Color SubtitleColor { get; set; }

        public Color TextColor { get; set; }

        public ICommand Command { get; set; }


        protected  void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

}