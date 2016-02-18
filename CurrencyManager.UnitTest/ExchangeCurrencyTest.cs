using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CurrencyManager.UnitTest
{
    [TestClass]
    public class ExchangeCurrencyTest
    {
        private const string CURRENCY_NAME = "CUR";
        private const string CURRENCY_NAME_1 = "CUR1";
        private const string CURRENCY_NAME_2 = "CUR2";
        private const string CURRENCY_NAME_3 = "CUR3";

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithNullFromCurrencyName_ShouldThrowArgumentNullException()
        {
            ExchangeCurrency.Create(null, null, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithEmptyFromCurrencyName_ShouldThrowArgumentNullException()
        {
            ExchangeCurrency.Create(string.Empty, null, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithOnlyWhiteSpaceFromCurrencyName_ShouldThrowArgumentNullException()
        {
            ExchangeCurrency.Create(" ", null, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithNullToCurrencyName_ShouldThrowArgumentNullException()
        {
            ExchangeCurrency.Create(CURRENCY_NAME, null, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithEmptyToCurrencyName_ShouldThrowArgumentNullException()
        {
            ExchangeCurrency.Create(CURRENCY_NAME, string.Empty, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithOnlyWhiteSpaceToCurrencyName_ShouldThrowArgumentNullException()
        {
            ExchangeCurrency.Create(CURRENCY_NAME, " ", 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_WithSameFromAndToCurrencyName_ShouldThrowArgumentException()
        {
            ExchangeCurrency.Create(CURRENCY_NAME, CURRENCY_NAME, 0);
        }

        [TestMethod]
        public void Equals_WithNull_ShouldReturnFalse()
        {
            var exchangeRate1 = ExchangeCurrency.Create(CURRENCY_NAME_1, CURRENCY_NAME_2, 0);

            Assert.IsFalse(exchangeRate1.Equals(null));
        }

        [TestMethod]
        public void Equals_WithSameCurrencyFromAndTo_ShouldReturnTrue()
        {
            var exchangeRate1 = ExchangeCurrency.Create(CURRENCY_NAME_1, CURRENCY_NAME_2, 0);
            var exchangeRate2 = ExchangeCurrency.Create(CURRENCY_NAME_1, CURRENCY_NAME_2, 0);

            Assert.IsTrue(exchangeRate1.Equals(exchangeRate2));
            Assert.IsTrue(exchangeRate2.Equals(exchangeRate1));
        }

        [TestMethod]
        public void Equals_WithSameCurrencyFromButNotTo_ShouldReturnFalse()
        {
            var exchangeRate1 = ExchangeCurrency.Create(CURRENCY_NAME_1, CURRENCY_NAME_2, 0);
            var exchangeRate2 = ExchangeCurrency.Create(CURRENCY_NAME_1, CURRENCY_NAME_3, 0);

            Assert.IsFalse(exchangeRate1.Equals(exchangeRate2));
            Assert.IsFalse(exchangeRate2.Equals(exchangeRate1));
        }

        [TestMethod]
        public void Equals_WithSameCurrencyToButNotFrom_ShouldReturnFalse()
        {
            var exchangeRate1 = ExchangeCurrency.Create(CURRENCY_NAME_1, CURRENCY_NAME_2, 0);
            var exchangeRate2 = ExchangeCurrency.Create(CURRENCY_NAME_3, CURRENCY_NAME_2, 0);

            Assert.IsFalse(exchangeRate1.Equals(exchangeRate2));
            Assert.IsFalse(exchangeRate2.Equals(exchangeRate1));
        }

        [TestMethod]
        public void CanChange_InitialCurrencyNotManaged_ShouldReturnFalse()
        {
            var exchangeRate = ExchangeCurrency.Create(CURRENCY_NAME_1, CURRENCY_NAME_2, 0);

            Assert.IsFalse(exchangeRate.CanChange(CURRENCY_NAME_3, CURRENCY_NAME_2));
        }

        [TestMethod]
        public void CanChange_TargetCurrencyNotManaged_ShouldReturnFalse()
        {
            var exchangeRate = ExchangeCurrency.Create(CURRENCY_NAME_1, CURRENCY_NAME_2, 0);

            Assert.IsFalse(exchangeRate.CanChange(CURRENCY_NAME_1, CURRENCY_NAME_3));
        }

        [TestMethod]
        public void CanChange_InitialAndTargetCurrencyManaged_ShouldReturnTrue()
        {
            var exchangeRate = ExchangeCurrency.Create(CURRENCY_NAME_1, CURRENCY_NAME_2, 0);

            Assert.IsTrue(exchangeRate.CanChange(CURRENCY_NAME_1, CURRENCY_NAME_2));
        }

        [TestMethod]
        public void CanChange_SwitchedInitialAndTargetCurrencyManaged_ShouldReturnTrue()
        {
            var exchangeRate = ExchangeCurrency.Create(CURRENCY_NAME_1, CURRENCY_NAME_2, 0);

            Assert.IsTrue(exchangeRate.CanChange(CURRENCY_NAME_2, CURRENCY_NAME_1));
        }
        
        [TestMethod]
        public void CanChangeFrom_CurrencyNotManaged_ShouldReturnFalse()
        {
            var exchangeRate = ExchangeCurrency.Create(CURRENCY_NAME_1, CURRENCY_NAME_2, 0);

            string changeTo;
            Assert.IsFalse(exchangeRate.CanChangeFrom(CURRENCY_NAME_3, out changeTo));
        }

        [TestMethod]
        public void CanChangeFrom_CurrencyNotManaged_OutParamShouldBeNull()
        {
            var exchangeRate = ExchangeCurrency.Create(CURRENCY_NAME_1, CURRENCY_NAME_2, 0);

            string changeTo;
            exchangeRate.CanChangeFrom(CURRENCY_NAME_3, out changeTo);

            Assert.IsNull(changeTo);
        }

        [TestMethod]
        public void CanChangeFrom_InitialCurrencyCorrespond_ShouldReturnTrue()
        {
            var exchangeRate = ExchangeCurrency.Create(CURRENCY_NAME_1, CURRENCY_NAME_2, 0);

            string changeTo;
            Assert.IsTrue(exchangeRate.CanChangeFrom(CURRENCY_NAME_1, out changeTo));
        }

        [TestMethod]
        public void CanChangeFrom_InitialCurrencyCorrespond_OutParamShouldBeEqualToTargetCurrency()
        {
            var exchangeRate = ExchangeCurrency.Create(CURRENCY_NAME_1, CURRENCY_NAME_2, 0);

            string changeTo;
            exchangeRate.CanChangeFrom(CURRENCY_NAME_1, out changeTo);

            Assert.AreEqual(exchangeRate.TargetCurrency, changeTo);
        }

        [TestMethod]
        public void CanChangeFrom_TargetCurrencyCorrespond_ShouldReturnTrue()
        {
            var exchangeRate = ExchangeCurrency.Create(CURRENCY_NAME_1, CURRENCY_NAME_2, 0);

            string changeTo;
            Assert.IsTrue(exchangeRate.CanChangeFrom(CURRENCY_NAME_2, out changeTo));
        }

        [TestMethod]
        public void CanChangeFrom_TargetCurrencyCorrespond_OutParamShouldBeEqualToInitialCurrency()
        {
            var exchangeRate = ExchangeCurrency.Create(CURRENCY_NAME_1, CURRENCY_NAME_2, 0);

            string changeTo;
            exchangeRate.CanChangeFrom(CURRENCY_NAME_2, out changeTo);

            Assert.AreEqual(exchangeRate.InitialCurrency, changeTo);
        }
    }
}
