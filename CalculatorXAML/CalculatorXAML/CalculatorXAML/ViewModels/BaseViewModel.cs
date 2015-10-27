using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CalculatorXAML.ViewModels
{
    /// <summary>
    /// Implements interface <see cref="INotifyPropertyChanged" /> event and its subscriber.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Member of interface <see cref="INotifyPropertyChanged" />. 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Is called when ViewModel property was changed. 
        /// </summary>
        /// <param name="propertyName">The name of property was changed.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
