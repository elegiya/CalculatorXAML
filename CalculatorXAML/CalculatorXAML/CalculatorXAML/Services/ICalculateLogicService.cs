using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorXAML.Services
{
    public interface ICalculateLogicService
    {
        decimal CalculateResult(decimal actualValue, char previousSymbol, string inputString);
    }
}
