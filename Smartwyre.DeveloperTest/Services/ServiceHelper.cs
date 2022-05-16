using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DeveloperTest.Data;

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

}