using System.Linq;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data
{
    public class AccountDataStore : IAccountDataStore
    {
        private readonly SmartwyreAppDbContext _context;

        public AccountDataStore(SmartwyreAppDbContext context)
        {
            _context = context;
        }

        public Account GetAccount(string accountNumber)
        {
            return _context.Accounts.FirstOrDefault(acc => acc.AccountNumber == accountNumber);
        }

        public void UpdateAccount(Account account)
        {
            _context.Update(account);
            _context.SaveChanges();
        }
    }
}
