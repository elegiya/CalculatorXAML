using System;
using CalculatorXAML.Services;
using NUnit.Framework;

namespace CalculatorXAML.Tests.Services
{
    [TestFixture]
    public class CalculateLogicServiceTest
    {
        private ICalculateLogicService _calculateLogicService;
        
        [TestFixtureSetUp]
        public void Initialize()
        {
            _calculateLogicService = new CalculateLogicService();
        }

        [Test]
        public void CalculateResult_TwoValuesSuccessfullyMinus()
        {
            //arrange
            var currentNumber = 45;
            var previousSymbol = '-';
            var inputString = "13";
            var expectedValue = 32;

            //act
            var actualValue = _calculateLogicService.CalculateResult(currentNumber, previousSymbol, inputString);

            //assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        public void CalculateResult_SymbolToCountInvalid_ValueBecomesTheLast()
        {
            //arrange
            var currentNumber = 22;
            var previousSymbol = '$';
            var inputString = "33";
            var expectedValue = 33;

            //act
            var actualValue = _calculateLogicService.CalculateResult(currentNumber, previousSymbol, inputString);

            //assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateResult_LastModifiedSymbolInvalid_Test()
        {
            //arrange
            var currentNumber = 56;
            var previousSymbol = 'x';
            var inputString = "2$";

            //act
            var actualValue = _calculateLogicService.CalculateResult(currentNumber, previousSymbol, inputString);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateResult_LastModifiedSymbolIsEmpty_Test()
        {
            //arrange
            var currentNumber = 56;
            var previousSymbol = '/';
            var inputString = string.Empty;

            //act
            var actualValue = _calculateLogicService.CalculateResult(currentNumber, previousSymbol, inputString);
        }

        [Test]
        [ExpectedException(typeof(DivideByZeroException))]
        public void CalculateResult_DivideByZero_ThrowsException()
        {//arrange
            var currentNumber = 45;
            var previousSymbol = '/';
            var inputString = "0";

            //act
            var actualValue = _calculateLogicService.CalculateResult(currentNumber, previousSymbol, inputString);
        }

        [Test]
        [ExpectedException(typeof(OverflowException))]
        public void CalculateResult_TooBigNumbers_ThrowsException()
        {//arrange
            var currentNumber = decimal.MinValue;
            var previousSymbol = 'x';
            var inputString = decimal.MaxValue.ToString();

            //act
            var actualValue = _calculateLogicService.CalculateResult(currentNumber, previousSymbol, inputString);
        }
    }
}