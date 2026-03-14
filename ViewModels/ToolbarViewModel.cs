using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Hercules.ViewModels
{
    public class ToolbarViewModel
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}