using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class ServiceHelper
{
    public static ServiceProvider ServiceProvider()
    {
        var collection = new ServiceCollection()
            .AddScoped<IPaymentService, PaymentService>()
            .AddScoped<IAccountDataStore, AccountDataStore>()
            .AddDbContext<SmartwyreAppDbContext>(opt => opt.UseInMemoryDatabase("SmartwyreDb"))
            .BuildServiceProvider();
        return collection;
    }

    public static void UpdateBalance(MakePaymentRequest request, Account account, IAccountDataStore dataStore)
    {
        account.Balance -= request.Amount;

        dataStore.UpdateAccount(account);

        var creditorAccount = dataStore.GetAccount(request.CreditorAccountNumber);
        creditorAccount.Balance += request.Amount;
        dataStore.UpdateAccount(creditorAccount);
    }

}