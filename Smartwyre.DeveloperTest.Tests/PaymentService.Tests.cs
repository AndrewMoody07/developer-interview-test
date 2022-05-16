using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Tests
{
    [TestFixture()]
    public class PaymentServiceTests
    {
        private SmartwyreAppDbContext _context;
        private IPaymentService _paymentService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var collection = ServiceHelper.ServiceProvider();

            _paymentService = collection.GetService<IPaymentService>();
            _context = collection.GetRequiredService<SmartwyreAppDbContext>();

            AccountSeedData.Initialise(_context);
        }

        [Test]
        public void Should_return_Success_when_paymentscheme_is_banktobanktransfer()
        {
            //Arrange
            var paymentRequest = new MakePaymentRequest()
            {
                Amount = 5.00m,
                CreditorAccountNumber = "123456",
                DebtorAccountNumber = "345678",
                PaymentDate = DateTime.UtcNow,
                PaymentScheme = PaymentScheme.BankToBankTransfer
            };

            var expectedResult = new MakePaymentResult() { Success = true };

            //Act
            var actualResult = _paymentService.MakePayment(paymentRequest);

            //Assert
            Assert.AreEqual(expectedResult.Success,actualResult.Success);
        }

        [Test]
        public void Should_not_return_Success_when_paymentscheme_is_banktobanktransfer_and_balance_is_too_low()
        {
            //Arrange
            var paymentRequest = new MakePaymentRequest()
            {
                Amount = 105.00m,
                CreditorAccountNumber = "123456",
                DebtorAccountNumber = "345678",
                PaymentDate = DateTime.UtcNow,
                PaymentScheme = PaymentScheme.BankToBankTransfer
            };

            var expectedResult = new MakePaymentResult() { Success = false };

            //Act
            var actualResult = _paymentService.MakePayment(paymentRequest);

            //Assert
            Assert.AreEqual(expectedResult.Success, actualResult.Success);
        }

        [Test]
        public void Should_not_return_Success_when_payment_request_is_null()
        {
            //Arrange
            var expectedResult = new MakePaymentResult() { Success = false };

            //Act
            var actualResult = _paymentService.MakePayment(null);

            //Assert
            Assert.AreEqual(expectedResult.Success, actualResult.Success);
        }

        [Test]
        public void Should_not_return_Success_when_account_does_not_have_enough_funds()
        {
            //Arrange
            var paymentRequest = new MakePaymentRequest()
            {
                Amount = 105.00m,
                CreditorAccountNumber = "123456",
                DebtorAccountNumber = "111111",
                PaymentDate = DateTime.UtcNow,
                PaymentScheme = PaymentScheme.ExpeditedPayments
            };

            var expectedResult = new MakePaymentResult() { Success = false };

            //Act
            var actualResult = _paymentService.MakePayment(paymentRequest);

            //Assert
            Assert.AreEqual(expectedResult.Success, actualResult.Success);
        }

        [Test]
        public void Should_not_return_Success_when_account_account_payment_scheme_is_not_allowed()
        {
            //Arrange
            var paymentRequest = new MakePaymentRequest()
            {
                Amount = 5.00m,
                CreditorAccountNumber = "123456",
                DebtorAccountNumber = "222222",
                PaymentDate = DateTime.UtcNow,
                PaymentScheme = PaymentScheme.ExpeditedPayments
            };

            var expectedResult = new MakePaymentResult() { Success = false };

            //Act
            var actualResult = _paymentService.MakePayment(paymentRequest);

            //Assert
            Assert.AreEqual(expectedResult.Success, actualResult.Success);
        }

        [Test]
        public void Should_not_return_Success_when_payment_request_payment_scheme_is_null()
        {
            //Arrange
            var paymentRequest = new MakePaymentRequest()
            {
                Amount = 5.00m,
                CreditorAccountNumber = "123456",
                DebtorAccountNumber = "222222",
                PaymentDate = DateTime.UtcNow
            };

            var expectedResult = new MakePaymentResult() { Success = false };

            //Act
            var actualResult = _paymentService.MakePayment(paymentRequest);

            //Assert
            Assert.AreEqual(expectedResult.Success, actualResult.Success);
        }
    }
}
