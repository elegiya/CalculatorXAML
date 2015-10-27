using System;

namespace CalculatorXAML.Services
{
    /// <summary>
    ///     Calculates a value by symbol choise.
    /// </summary>
    public class CalculateLogicService : ICalculateLogicService
    {
        #region Private fields

        private const char PLUS_SYMBOL = '+';
        private const char MINUS_SYMBOL = '-';
        private const char MULTIPLY_SYMBOL = 'x';
        private const char DIVIDE_SYMBOL = '/';
        private const char DOT_SYMBOL = '.';
        private const char ZERO_SYMBOL = '0';

        #endregion

        #region Interface method implementation

        /// <summary>
        ///     Calculates the result between operations when symbol chosen.
        /// </summary>
        /// <param name="actualValue">Value was actual before symbol button was pressed.</param>
        /// <param name="previousSymbol">Symbol to make operation.</param>
        /// <param name="inputString">New entered string as number to work with.</param>
        /// <returns>Result of the last operation.</returns>
        public decimal CalculateResult(decimal actualValue, char previousSymbol, string inputString)
        {
            decimal newValue;
            if (!decimal.TryParse(inputString, out newValue))
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

    #endregion
}