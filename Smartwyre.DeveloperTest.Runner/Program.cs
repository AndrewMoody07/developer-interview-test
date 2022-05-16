using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Runner
{
    internal class Program
    {
        private static void Main()
        {
            var collection = ServiceHelper.ServiceProvider();

            var paymentService = collection.GetService<IPaymentService>();
            var context = collection.GetRequiredService<SmartwyreAppDbContext>();

            // Seed the in-memory database
            AccountSeedData.Initialise(context);

            paymentService?.MakePayment(new MakePaymentRequest
            {
                Amount = 2.0m,
                CreditorAccountNumber = "123456",
                DebtorAccountNumber = "345678",
                PaymentDate = DateTime.UtcNow,
                PaymentScheme = PaymentScheme.BankToBankTransfer
            });

            foreach (var account in context.Accounts)
            {
                Console.WriteLine($"Id:{account.Id} --> Balance: {account.Balance}");
            }
        }
    }
}
