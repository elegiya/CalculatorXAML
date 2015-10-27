using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalculatorXAML.Services;
using CalculatorXAML.ViewModels;
using Moq;
using NUnit.Framework;

namespace CalculatorXAML.Tests.ViewModels
{
    [TestFixture]
    public class CalculatorVewModelTest
    {
        private CalculatorVewModel _calculatorViewModel;
        private const decimal RETURNED_VALUE_FROM_SERVICE = 28;

        [TestFixtureSetUp]
        public void Initialize()
        {
            _calculatorViewModel = new CalculatorVewModel();
        }

        [Test]
        public void ChooseDigitCommand_AddedDigitToCountedStrings()
        {
            //arrange
            _calculatorViewModel.InputString = "12";
            _calculatorViewModel.HistoryString = "23+44-12";


            //act
            _calculatorViewModel.ChooseDigitCommand.Execute("5");
            
            //assert
            Assert.AreEqual(_calculatorViewModel.InputString, "125");
            Assert.AreEqual(_calculatorViewModel.HistoryString, "23+44-125");
        }

        [Test]
        public void ChooseDigitCommand_CleanInputStringAfterEqual()
        {
            //arrange
            _calculatorViewModel.InputString = "12";
            _calculatorViewModel.HistoryString = "23+44-12";
            _calculatorViewModel.PreviousSymbol = '=';

            //act
            _calculatorViewModel.ChooseDigitCommand.Execute("5");

            //assert
            Assert.AreEqual(_calculatorViewModel.InputString, "5");
            Assert.AreEqual(_calculatorViewModel.HistoryString, "23+44-125");
        }

        [Test]
        public void ChooseSymbolCommand_ModifyInputHistoryStrings()
        {
            //arrange
            var _calculateLogicServiceMock = new Mock<ICalculateLogicService>();
            _calculateLogicServiceMock.Setup(
                c => c.CalculateResult(It.IsAny<decimal>(), It.IsAny<char>(), It.IsAny<string>()))
                .Returns(RETURNED_VALUE_FROM_SERVICE);
            
            _calculatorViewModel.InputString = "12";
            _calculatorViewModel.HistoryString = "23+44-12";

            var actualSymbol = '-';

            var expectedInputString = string.Empty;
            var expectedHistoryString = $"{_calculatorViewModel.HistoryString}{actualSymbol}";

            //act
            _calculatorViewModel.ChooseSymbolCommand.Execute("-");

            //assert
            Assert.AreEqual(_calculatorViewModel.InputString, expectedInputString);
            Assert.AreEqual(_calculatorViewModel.HistoryString, expectedHistoryString);
        }
    }
}
