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
        public void ChooseEqualCommand_Strings()
        {
            //arrange
            var _calculateLogicServiceMock = new Mock<ICalculateLogicService>();
            _calculateLogicServiceMock.Setup(
                c => c.CalculateResult(It.IsAny<decimal>(), It.IsAny<char>(), It.IsAny<string>()))
                .Returns(RETURNED_VALUE_FROM_SERVICE);
            
            _calculatorViewModel.PreviousValue = 11;
            _calculatorViewModel.PreviousSymbol = '+';
            _calculatorViewModel.InputString = "45";
            _calculatorViewModel.HistoryString = "23+44-12";
            
            var expectedPreviousValue = 56;
            var expectedPreviousSymbol = '=';
            var expectedInputString = expectedPreviousValue.ToString();
            var expectedHistoryString = string.Empty;

            //act
            _calculatorViewModel.ChooseEqualCommand.Execute("=");

            //assert
            Assert.AreEqual(_calculatorViewModel.InputString, expectedInputString);
            Assert.AreEqual(_calculatorViewModel.PreviousValue, expectedPreviousValue);
            Assert.AreEqual(_calculatorViewModel.PreviousSymbol, expectedPreviousSymbol);
            Assert.AreEqual(_calculatorViewModel.HistoryString, expectedHistoryString);
        }

        [Test]
        public void ChooseCancelCommand_CleansPreviousValues()
        {
            //arrange
            var _calculateLogicServiceMock = new Mock<ICalculateLogicService>();
            _calculateLogicServiceMock.Setup(
                c => c.CalculateResult(It.IsAny<decimal>(), It.IsAny<char>(), It.IsAny<string>()))
                .Returns(RETURNED_VALUE_FROM_SERVICE);
            
            var expectedInputString = string.Empty;
            var expectedPreviousValue = 0;
            var expectedPreviousSymbol = char.MinValue;
            var expectedHistoryString = string.Empty;

            _calculatorViewModel.InputString = "test string";
            _calculatorViewModel.PreviousValue = 23;
            _calculatorViewModel.PreviousSymbol = 'l';
            _calculatorViewModel.HistoryString = "2+2";

            //act
            _calculatorViewModel.ChooseCancelCommand.Execute("-");

            //assert
            Assert.AreEqual(_calculatorViewModel.InputString, expectedInputString);
            Assert.AreEqual(_calculatorViewModel.PreviousValue, expectedPreviousValue);
            Assert.AreEqual(_calculatorViewModel.PreviousSymbol, expectedPreviousSymbol);
            Assert.AreEqual(_calculatorViewModel.HistoryString, expectedHistoryString);
        }
    }
}
