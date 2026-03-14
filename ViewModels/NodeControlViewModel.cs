using System.ComponentModel;
using System.Runtime.CompilerServices;
using Hercules.Interface;

namespace Hercules.ViewModels
{
    public class NodeControlViewModel : INotifyPropertyChanged, IPositionAble
    {

        public event PropertyChangedEventHandler? PropertyChanged;
        private double _x;
        private double _y;
        private double _width;
        private double _height;
        private string? _title;


        /**
         * 这里 加一个 ? 就是可以允许 null
         */
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public double x
        {
            get => _x;
            set
            {
                if (_x != value)
                {
                    _x = value;
                    OnPropertyChanged();
                }
            }
        }

        public double y
        {
            get => _y;
            set
            {
                if (_y != value)
                {
                    _y = value;
                    OnPropertyChanged();
                }
            }
        }


        public string? title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged();
                }
            }
        }

        public double width
        {
            get => _width;
            set
            {
                if (_width != value)
                {
                    _width = value;
                    OnPropertyChanged();
                }
            }
        }

        public double height
        {
            get => _height;
            set
            {
                if (value != _height)
                {
                    _height = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}

