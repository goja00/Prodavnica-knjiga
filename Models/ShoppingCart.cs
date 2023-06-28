using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bookverse.Models
{
	public class ShoppingCart
	{		
		public int Id { get; set; }
		public int ProductId { get; set; }
		[ForeignKey("Id")]
		[ValidateNever]
		public Product Prod { get; set; }

		[Range(1, 1000, ErrorMessage = "Please enter value between 1 and 1000")]
		public int Count = 0;

		public string ApplicationUserId { get; set; }
		[ForeignKey("ApplicationUserId")]
		[ValidateNever]
		public ApplicationUser ApplicationUser { get; set; }
	}
}
