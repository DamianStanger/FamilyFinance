using System.ComponentModel.DataAnnotations;

namespace FamilyFinance.Models.Finance
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public virtual Category ParentCategory { get; set; }
    }
}