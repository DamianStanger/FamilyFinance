namespace FamilyFinance.Models.Domain
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public virtual Category ParentCategory { get; set; }
    }
}