using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Models;

public class Product
{
    [Key]
    public int productId { get; set; }

    [Display(Name = "Name")]
    public string productName { get; set; }
    public int supplierId { get; set; }
    public int categoryId { get; set; }
    public string quantityPerUnit { get; set; }
    [Display(Name = "Unit Price")]
    public decimal unitPrice { get; set; }
    [Display(Name = "In Stock")]
    public int unitsInStock { get; set; }
    [Display(Name = "On Order")]
    public int unitsOnOrder { get; set; }
    public int reorderLevel { get; set; }
    public bool discontinued { get; set; }
    [ForeignKey("categoryId")]
    public Category? category { get; set; }
}



