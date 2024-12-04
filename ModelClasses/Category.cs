using System.ComponentModel.DataAnnotations;

namespace ModelClasses
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Category Name cannot be longer than 30 characters")]
        public string Name { get; set; }
    }
}
