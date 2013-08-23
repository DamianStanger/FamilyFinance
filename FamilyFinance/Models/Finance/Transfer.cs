namespace FamilyFinance.Models.Finance
{
    public class Transfer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public int FromAccountId { get; set; }
        public virtual Account FromAccount { get; set; }
        public int ToAccountId { get; set; }
        public virtual Account ToAccount { get; set; }
    }
}