namespace FamilyFinance.Models.Domain
{
    public class AccountType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public const int Debit = 1;
        public const int Savings = 2;
        public const int Credit = 3;
        public const int Loan = 4;
    }
}