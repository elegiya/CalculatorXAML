using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using CalculatorXAML.Services;
using Xamarin.Forms;

namespace CalculatorXAML.ViewModels
{
    public class CalculatorVewModel : INotifyPropertyChanged
    {
        private readonly ICalculateLogicService _calculateLogic;

        private string _formedString;
        private string _historyString;
        private decimal _previousValue;
        private char _previousSymbol;

        public CalculatorVewModel()
        {
            _calculateLogic = new CalculateLogicService();

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
                HistoryString += string.Format(" {0} ", key);
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // ICommand implementations
        public ICommand ChooseDigitCommand { protected set; get; }
        public ICommand ChooseSymbolCommand { protected set; get; }
        public ICommand ChooseEqualCommand { protected set; get; }
        public ICommand ChooseCancelCommand { protected set; get; }

        private void CalculateResult(string key)
        {
            try
            {
                PreviousValue = _calculateLogic.CalculateResult(PreviousValue, PreviousSymbol, InputString);
            }
            catch (DivideByZeroException ex)
            {
                Alert("You can not divide by Zero! Please, do another operation!");
            }
            catch (OverflowException ex)
            {
                Alert("The number is too much to count! Please, enter another number!");
            }
            catch (ArgumentException ex)
            {
                Alert("You enter not a valid number! Please, enter another number!");
            }

            PreviousSymbol = key[0];
        }

        private async void Alert(string message, string title = "Error")
        {
            await UserDialogs.Instance?.AlertAsync(message, title);
        }
    }
}
