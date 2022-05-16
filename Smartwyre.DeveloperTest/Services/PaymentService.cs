using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        public SmartwyreAppDbContext Context { get; }
        private readonly IAccountDataStore _dataStore;

        public PaymentService(SmartwyreAppDbContext context, IAccountDataStore dataStore)
        {
            Context = context;
            _dataStore = dataStore;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var result = new MakePaymentResult();

            if (request == null)
            {
                result.Success = false;
                return result;
            }

            var account = _dataStore.GetAccount(request.DebtorAccountNumber);

            if (account == null)
            {
                result.Success = false;
                return result;
            }

            switch (request.PaymentScheme)
            {
                case PaymentScheme.BankToBankTransfer:
                    if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.BankToBankTransfer) ||
                        account.Balance < request.Amount)
                    {
                        result.Success = false;
                    }
                    break;

                case PaymentScheme.ExpeditedPayments:
                    if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.ExpeditedPayments) ||
                        account.Balance < request.Amount)
                    {
                        result.Success = false;
                    }

                    break;

                case PaymentScheme.AutomatedPaymentSystem:
                    if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.AutomatedPaymentSystem) ||
                        account.Status != AccountStatus.Live)
                    {
                        result.Success = false;
                    }

                    break;
                default:
                    {
                        result.Success = false;
                        break;
                    }
            }

            if (!result.Success) return result;

            ServiceHelper.UpdateBalance(request, account, _dataStore);

            return result;
        }
    }
}
