using System.Linq;

namespace FamilyFinance.Models.ViewModel
{
    public class StatementViewModel
    {
        public string AccountName { get; set; }
        public IQueryable<FamilyFinance.Models.Domain.Transaction> Transactions { get; set; }
        public string StatementDate { get; set; }
    }
}