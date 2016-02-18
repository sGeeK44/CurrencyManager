using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CurrencyManager.UnitTest
{
    [TestClass]
    public class ExchangeChainFactoryTest
    {
        private const string CURRENCY_NAME_1 = "CUR1";
        private const string CURRENCY_NAME_2 = "CUR2";

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullAvailableExchangeCurrency_ShouldThrowArgumentNullException()
        {
            new ExchangeChainFactory(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_EmptyAvailableExchangeCurrency_ShouldThrowArgumentNullException()
        {
            new ExchangeChainFactory(new List<IExchangeCurrency>());
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void Create_NullInitialCurrency_ShouldThrowArgumentNullException()
        {
            var factory = new ExchangeChainFactory(new List<IExchangeCurrency>{ new ExchangeCurrencyStub() });
            Assert.IsNull(factory.Create(null, null));
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void Create_EmptyInitialCurrency_ShouldThrowArgumentNullException()
        {
            var factory = new ExchangeChainFactory(new List<IExchangeCurrency> { new ExchangeCurrencyStub() });
            Assert.IsNull(factory.Create(string.Empty, null));
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void Create_NullTargetCurrency_ShouldThrowArgumentNullException()
        {
            var factory = new ExchangeChainFactory(new List<IExchangeCurrency> { new ExchangeCurrencyStub() });
            Assert.IsNull(factory.Create(CURRENCY_NAME_1, null));
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void Create_EmptyTargetCurrency_ShouldThrowArgumentNullException()
        {
            var factory = new ExchangeChainFactory(new List<IExchangeCurrency> { new ExchangeCurrencyStub() });
            Assert.IsNull(factory.Create(CURRENCY_NAME_1, string.Empty));
        }

        [TestMethod]
        public void Create_AvailableListCanNotChange_SouldReturnNullChain()
        {
            var availableExchangeList = new List<IExchangeCurrency>
            {
                ExchangeCurrencyStub.CreateExchangeWichCanDoNothing()
            };

            var factory = new ExchangeChainFactory(availableExchangeList);
            Assert.IsNull(factory.Create(CURRENCY_NAME_1, CURRENCY_NAME_2));
        }

        [TestMethod]
        public void Create_AvailableListContainsOneElementCanChangeDirectly_ChangeInvolveShouldBeEqualToOne()
        {
            var availableExchangeList = new List<IExchangeCurrency>
            {
                ExchangeCurrencyStub.CreateExchangeWichManageOnlyBothCurrency()
            };

            var factory = new ExchangeChainFactory(availableExchangeList);
            var exchangeChain = factory.Create(CURRENCY_NAME_1, CURRENCY_NAME_2);

            Assert.AreEqual(1, exchangeChain.CountIntermediateChangeNeeded());
        }
    }
}
