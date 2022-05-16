using System.ComponentModel.DataAnnotations;

namespace Smartwyre.DeveloperTest.Types
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public AccountStatus Status { get; set; }
        public AllowedPaymentSchemes AllowedPaymentSchemes { get; set; }
    }
}
