﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CurrencyManager.UnitTest
{
    [TestClass]
    public class ExchangeChainFactoryTest
    {
        private const string CURRENCY_NAME_1 = "CUR1";
        private const string CURRENCY_NAME_2 = "CUR2";
        private const string CURRENCY_NAME_3 = "CUR3";
        private const string CURRENCY_NAME_4 = "CUR4";

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

            Assert.AreEqual(1, exchangeChain.CountPartInvolve);
        }

        [TestMethod]
        public void Create_AvailableListContainsTwoElementCanChangeWithIntermediate_ChangeInvolveShouldBeEqualToTwo()
        {
            var firstExchange = CreateIntermediateExchange(CURRENCY_NAME_1, CURRENCY_NAME_2, CURRENCY_NAME_3);
            var secondExchange = CreateFinalExchange(CURRENCY_NAME_2, CURRENCY_NAME_3);
            var availableExchangeList = new List<IExchangeCurrency> { firstExchange, secondExchange };

            var factory = new ExchangeChainFactory(availableExchangeList);
            var exchangeChain = factory.Create(CURRENCY_NAME_1, CURRENCY_NAME_3);

            Assert.AreEqual(2, exchangeChain.CountPartInvolve);
        }

        [TestMethod]
        public void Create_AvailableListContainsTwoElementCanChangeWithIntermediateFinalSwitched_ChangeInvolveShouldBeEqualToTwo()
        {
            var firstExchange = CreateIntermediateExchange(CURRENCY_NAME_1, CURRENCY_NAME_2, CURRENCY_NAME_3);
            var secondExchange = CreateFinalExchange(CURRENCY_NAME_3, CURRENCY_NAME_2);
            var availableExchangeList = new List<IExchangeCurrency> { firstExchange, secondExchange };

            var factory = new ExchangeChainFactory(availableExchangeList);
            var exchangeChain = factory.Create(CURRENCY_NAME_1, CURRENCY_NAME_3);

            Assert.AreEqual(2, exchangeChain.CountPartInvolve);
        }

        [TestMethod]
        public void Create_AvailableListContainsThreeElementCanChangeWithIntermediate_ChangeInvolveShouldBeEqualToThree()
        {
            var firstExchange = CreateIntermediateExchange(CURRENCY_NAME_1, CURRENCY_NAME_2, CURRENCY_NAME_4);
            var secondExchange = CreateIntermediateExchange(CURRENCY_NAME_2, CURRENCY_NAME_3, CURRENCY_NAME_4);
            var third = CreateFinalExchange(CURRENCY_NAME_3, CURRENCY_NAME_4);
            var availableExchangeList = new List<IExchangeCurrency> { firstExchange, secondExchange, third };

            var factory = new ExchangeChainFactory(availableExchangeList);
            var exchangeChain = factory.Create(CURRENCY_NAME_1, CURRENCY_NAME_4);

            Assert.AreEqual(3, exchangeChain.CountPartInvolve);
        }

        [TestMethod]
        public void ToString_DirectExchange_ShoudReturnPath()
        {
            var directExchange = DirectExchange.Create(new ExchangeCurrencyStub(), "INI", "TAR");
            Assert.AreEqual("INI;TAR", directExchange.ToString());
        }

        [TestMethod]
        public void ToString_ThroughExchange_ShoudReturnPath()
        {
            var directExchange = ThroughExchange.Create(ExchangeCurrency.Create("INI", "INT", 1), "INI", "TAR", new List<IExchangeCurrency>()
            {
                ExchangeCurrency.Create("INT", "TAR", 1),
            });

            Assert.AreEqual("INI;INT;TAR", directExchange.ToString());
        }

        [TestMethod]
        public void FactoryCreate_Loop_ShouldReturnRightPath()
        {
            var availableExchangeCurrency = new List<IExchangeCurrency>
            {
                ExchangeCurrency.Create("EUR", "USD", 1),
                ExchangeCurrency.Create("USD", "LIV", 1),
                ExchangeCurrency.Create("USD", "CHF", 1),
                ExchangeCurrency.Create("USD", "AUD", 1),
                ExchangeCurrency.Create("CHF", "LIV", 1),
                ExchangeCurrency.Create("CHF", "AUD", 1),
                ExchangeCurrency.Create("CHF", "AUD", 1),
                ExchangeCurrency.Create("LIV", "AUD", 1),
                ExchangeCurrency.Create("JPY", "LIV", 1)
            };

            var chainFactory = new ExchangeChainFactory(availableExchangeCurrency);
            Assert.AreEqual("EUR;USD;LIV;JPY", chainFactory.Create("EUR", "JPY").ToString());
        }

        [TestMethod]
        public void FactoryCreate_SameCurrencyTwice_ShouldReturnRightPath()
        {
            var availableExchangeCurrency = new List<IExchangeCurrency>
            {
                ExchangeCurrency.Create("AAA", "BBB", 1),
                ExchangeCurrency.Create("BBB", "CCC", 1),
                ExchangeCurrency.Create("CCC", "BBB", 1)
            };

            var chainFactory = new ExchangeChainFactory(availableExchangeCurrency);
            Assert.AreEqual("AAA;BBB;CCC", chainFactory.Create("AAA", "CCC").ToString());
        }

        private static IExchangeCurrency CreateIntermediateExchange(string init, string intermediate, string target)
        {
            var mock = new Mock<IExchangeCurrency>();
            var firstChangeToCurrency = intermediate;
            mock.Setup(_ => _.CanOnlyMakeIntermediateChange(init, target)).Returns(true);
            mock.Setup(_ => _.CanOnlyMakeIntermediateChange(target, init)).Returns(true);
            mock.Setup(_ => _.CanChangeFrom(init, out firstChangeToCurrency)).Returns(true);
            return mock.Object;
        }

        private static IExchangeCurrency CreateFinalExchange(string init, string target)
        {
            var mock = new Mock<IExchangeCurrency>();
            mock.Setup(_ => _.CanChange(init, target)).Returns(true);
            mock.Setup(_ => _.CanChange(target, init)).Returns(true);
            return mock.Object;
        }
    }
}
