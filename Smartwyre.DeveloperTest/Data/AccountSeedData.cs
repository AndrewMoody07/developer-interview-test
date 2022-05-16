using Smartwyre.DeveloperTest.Types;
using System.Linq;

namespace Smartwyre.DeveloperTest.Data;

public class AccountSeedData
{
    public static void Initialise(SmartwyreAppDbContext context)
    {
        if (context.Accounts.Any()) return;

        context.Accounts.AddRange(new Account
            {
                Id = 1,
                AccountNumber = "123456",
                AllowedPaymentSchemes = AllowedPaymentSchemes.BankToBankTransfer,
                Balance = 0m,
                Status = AccountStatus.Live
            },
            new Account
            {
                Id = 2,
                AccountNumber = "345678",
                AllowedPaymentSchemes = AllowedPaymentSchemes.BankToBankTransfer,
                Balance = 100.00m,
                Status = AccountStatus.Live
            },
            new Account
            {
                Id = 3,
                AccountNumber = "111111",
                AllowedPaymentSchemes = AllowedPaymentSchemes.ExpeditedPayments,
                Balance = 100.00m,
                Status = AccountStatus.Live
            },
            new Account
            {
                Id = 4,
                AccountNumber = "222222",
                AllowedPaymentSchemes = AllowedPaymentSchemes.AutomatedPaymentSystem,
                Balance = 100.00m,
                Status = AccountStatus.Live
            });

        context.SaveChanges(true);
    }
}