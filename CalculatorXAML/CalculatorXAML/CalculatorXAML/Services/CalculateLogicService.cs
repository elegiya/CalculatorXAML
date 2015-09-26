using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorXAML.Services
{
    public class CalculateLogicService: ICalculateLogicService
    {
        private const char PLUS_SYMBOL = '+' ;
        private const char MINUS_SYMBOL = '-';
        private const char MULTIPLY_SYMBOL = 'x';
        private const char DIVIDE_SYMBOL = '/';
        private const char DOT_SYMBOL = '.';
        private const char ZERO_SYMBOL = '0';

        public decimal CalculateResult(decimal actualValue, char previousSymbol, string inputString)
        {
            decimal newValue;
            if (!Decimal.TryParse(inputString, out newValue))
            {
                throw new ArgumentException();
            }
            if (previousSymbol == DOT_SYMBOL)
            {
                actualValue += ZERO_SYMBOL;
            }

            switch (previousSymbol)
            {
                case (PLUS_SYMBOL):
                    actualValue += newValue;
                    break;
                case (MINUS_SYMBOL):
                    actualValue -= newValue;
                    break;
                case (MULTIPLY_SYMBOL):
                    actualValue *= newValue;
                    break;
                case (DIVIDE_SYMBOL):
                    if (newValue == 0)
                    {
                        throw new DivideByZeroException();
                    }
                    actualValue /= newValue;
                    break;
                default:
                    actualValue = newValue;
                    break;
            }

            return actualValue;
        }
    }
}
