using System;
using System.Windows.Input;
using Acr.UserDialogs;
using CalculatorXAML.Services;
using Xamarin.Forms;

namespace CalculatorXAML.ViewModels
{
    public class CalculatorVewModel : BaseViewModel
    {
        #region Ctor

        public CalculatorVewModel()
        {
            _calculateLogic = new CalculateLogicService();

            ChooseDigitCommand = new Command<string>(OnDigitChosen());
            ChooseSymbolCommand = new Command<string>(OnSymbolChosen());
            ChooseEqualCommand = new Command<string>(OnEqualsChosen());
            ChooseCancelCommand = new Command<string>(OnCancelChosen());
        }

        #endregion

        #region Private fields

        private readonly ICalculateLogicService _calculateLogic;
        private string _formedString;
        private string _historyString;
        private decimal _previousValue;
        private char _previousSymbol;
        private const char EQUAL_SYMBOL = '=';

        #endregion

        #region Properties

        public string InputString
        {
            get { return _formedString; }
            set
            {
                if (_formedString != value)
                {
                    _formedString = value;
                    OnPropertyChanged();
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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region ICommand implementations

        public ICommand ChooseDigitCommand { protected set; get; }
        public ICommand ChooseSymbolCommand { protected set; get; }
        public ICommand ChooseEqualCommand { protected set; get; }
        public ICommand ChooseCancelCommand { protected set; get; }

        #endregion

        #region Private Methods for Command Subscribing

        private Action<string> OnDigitChosen()
        {
            return key =>
            {
                if (PreviousSymbol == EQUAL_SYMBOL)
                {
                    InputString = string.Empty;
                }

                InputString += key;
                HistoryString += key;
            };
        }

        private Action<string> OnSymbolChosen()
        {
            return key =>
            {
                CalculateResult(key);
                InputString = string.Empty;
                HistoryString += string.Format(" {0} ", key);
            };
        }

        private Action<string> OnEqualsChosen()
        {
            return key =>
            {
                CalculateResult(key);
                InputString = PreviousValue.ToString();
                HistoryString = string.Empty;
            };
        }

        private Action<string> OnCancelChosen()
        {
            return nothing =>
            {
                InputString = string.Empty;
                PreviousValue = 0;
                PreviousSymbol = char.MinValue;
                HistoryString = string.Empty;
            };
        }

        #endregion

        #region Alerts for UI

        private void CalculateResult(string key)
        {
            try
            {
                PreviousValue = _calculateLogic.CalculateResult(PreviousValue, PreviousSymbol, InputString);
            }
            catch (DivideByZeroException)
            {
                Alert("You can not divide by Zero! Please, do another operation!");
            }
            catch (OverflowException)
            {
                Alert("The number is too much to count! Please, enter another number!");
            }
            catch (ArgumentException)
            {
                Alert("You enter not a valid number! Please, enter another number!");
            }

            PreviousSymbol = key[0];
        }

        private async void Alert(string message, string title = "Error")
        {
            await UserDialogs.Instance?.AlertAsync(message, title);
        }

        #endregion
    }
}