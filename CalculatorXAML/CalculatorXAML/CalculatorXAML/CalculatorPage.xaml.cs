using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace CalculatorXAML
{
    /// <summary>
    /// Created a new main calculator page
    /// </summary>
    public partial class CalculatorPage : ContentPage
    {
        private string m_formedString;
        private string m_historyString;
        private decimal m_previousValue;
        private char m_previousSymbol;

        public CalculatorPage()
        {
            InitializeComponent();
        }

        private void ButtonNumber_Click(object sender, EventArgs e)
        {
            Button senderButton = (Button)sender;
            if (m_previousSymbol == '=')
            {
                m_formedString = string.Empty;
            }

            m_formedString += senderButton.Text;
            m_historyString += senderButton.Text;

            previousResultLabel.Text = m_historyString;
            resultLabel.Text = m_formedString;
        }

        private void ButtonSymbol_Click(object sender, EventArgs e)
        {
            Button senderButton = (Button)sender;
            char selectedSymbol = senderButton.Text[0];
            decimal newValue;
            if (!Decimal.TryParse(m_formedString, out newValue))
            {
                DisplayAlert("Error", "Please, enter a valid number", "OK");
                return;
            }
            switch (m_previousSymbol)
            {
                case ('+'):
                    m_previousValue += newValue;
                    break;
                case ('-'):
                    m_previousValue -= newValue;
                    break;
                case ('x'):
                    m_previousValue *= newValue;
                    break;
                case ('/'):
                    if (newValue == 0)
                    {
                        DisplayAlert("Error", "You can not divide by zero!", "OK");
                        return;
                    }
                    m_previousValue /= newValue;
                    break;
                default:
                    m_previousValue = newValue;
                    break;
            }

            m_historyString += string.Format(" {0} ", selectedSymbol);
            previousResultLabel.Text = m_historyString;

            m_previousSymbol = selectedSymbol;
            m_formedString = string.Empty;
        }

        private void ButtonEqual_Click(object sender, EventArgs e)
        {
            ButtonSymbol_Click(sender, e);
            m_formedString = m_previousValue.ToString();

            m_historyString = m_previousValue.ToString();
            m_previousSymbol = ((Button) sender).Text[0];
            resultLabel.Text = m_previousValue.ToString();
            previousResultLabel.Text = m_previousValue.ToString();
        }

        private void ButtonCancel_Clicked(object sender, EventArgs e)
        {
            m_formedString = string.Empty;
            m_previousValue = 0;
            m_previousSymbol = char.MinValue;
            m_historyString=string.Empty;
            previousResultLabel.Text = m_historyString;
            resultLabel.Text = string.Empty;
        }
    }
}
