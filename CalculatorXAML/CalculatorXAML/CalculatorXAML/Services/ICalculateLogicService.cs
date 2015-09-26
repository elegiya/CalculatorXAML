using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorXAML.Services
{
    /// <summary>
    /// Calculates a value by symbol choise.
    /// </summary>
    public interface ICalculateLogicService
    {
        /// <summary>
        /// Calculates the result between operations when symbol chosen.
        /// </summary>
        /// <param name="actualValue">Value was actual before symbol button was pressed.</param>
        /// <param name="previousSymbol">Symbol to make operation.</param>
        /// <param name="inputString">New entered string as number to work with.</param>
        /// <returns>Result of the last operation.</returns>
        decimal CalculateResult(decimal actualValue, char previousSymbol, string inputString);
    }
}
