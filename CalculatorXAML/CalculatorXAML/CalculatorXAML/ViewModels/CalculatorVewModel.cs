using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CalculatorXAML.ViewModels
{
    public class CalculatorVewModel : INotifyPropertyChanged
    {
        private string _formedString;
        private string _historyString;
        private decimal _previousValue;
        private char _previousSymbol;

        public CalculatorVewModel()
        {
            this.ChooseDigitCommand = new Command<string>((key) =>
            {
                if (PreviousSymbol == '=')
                {
                    this.InputString = string.Empty;
                }

                this.InputString += key;
                this.HistoryString += key;
            });

            this.ChooseSymbolCommand = new Command<string>((key) =>
            {
                CalculateResult(key);
                InputString = string.Empty;
            });

            this.ChooseEqualCommand = new Command<string>((key) =>
            {
                CalculateResult(key);
                InputString = PreviousValue.ToString();
                HistoryString = string.Empty;
            });

            this.ChooseCancelCommand = new Command<string>((nothing) =>
            {
                InputString = string.Empty;
                PreviousValue = 0;
                PreviousSymbol = char.MinValue;
                HistoryString = string.Empty;
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string InputString
        {
            get { return _formedString; }
            set
            {
                if (_formedString != value)
                {
                    _formedString = value;
                    OnPropertyChanged("InputString");
                }
            }
        }

        public string HistoryString
        {
            get { return _historyString; }
            set
            {
                if (_historyString != value)
                {
                    _historyString = value;
                    OnPropertyChanged("HistoryString");
                }
            }
        }

        public decimal PreviousValue
        {
            get { return _previousValue; }
            set
            {
                if (_previousValue != value)
                {
                    _previousValue = value;
                    OnPropertyChanged("PreviousValue");
                }
            }
        }

        public char PreviousSymbol
        {
            get { return _previousSymbol; }
            set
            {
                if (_previousSymbol != value)
                {
                    _previousSymbol = value;
                    OnPropertyChanged("PreviousSymbol");
                }
            }
        }
        
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }

        // ICommand implementations
        public ICommand ChooseDigitCommand { protected set; get; }
        public ICommand ChooseSymbolCommand { protected set; get; }
        public ICommand ChooseEqualCommand { protected set; get; }
        public ICommand ChooseCancelCommand { protected set; get; }

        private void CalculateResult(string key)
        {
            char selectedSymbol = key[0];
            decimal newValue;
            if (!Decimal.TryParse(InputString, out newValue))
            {
                //DisplayAlert("Error", "Please, enter a valid number", "OK");
                return;
            }
            if (PreviousSymbol == '.')
            {
                PreviousValue += '0';
            }
            try
            {
                switch (PreviousSymbol)
                {
                    case ('+'):
                        PreviousValue += newValue;
                        break;
                    case ('-'):
                        PreviousValue -= newValue;
                        break;
                    case ('x'):
                        PreviousValue *= newValue;
                        break;
                    case ('/'):
                        if (newValue == 0)
                        {
                            //DisplayAlert("Error", "You can not divide by zero!", "OK");
                            return;
                        }
                        PreviousValue /= newValue;
                        break;
                    default:
                        PreviousValue = newValue;
                        break;
                }
            }
            catch (OverflowException ex)
            {
                //displayalert
            }
            PreviousSymbol = selectedSymbol;
            HistoryString += string.Format(" {0} ", selectedSymbol);
        }
    }
}
