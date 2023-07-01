using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookverse.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        [Range(1, 10000)]
        public double Price { get; set; }

        [Required]
        [ValidateNever]
        public string ImageURL { get; set; }

        [Required]

        public int CategoryID { get; set; }

        [ValidateNever]
        public Category c { get; set; }
        [Required]

        public int CoverTypeID { get; set; }
        [ValidateNever]
        public CoverType ct { get; set; }

        [NotMapped]
        public int Count { get; set; }
    }
}

