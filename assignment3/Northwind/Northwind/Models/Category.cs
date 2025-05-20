using System.ComponentModel.DataAnnotations;

namespace Northwind.Models;

public class Category
{
    [Key]
    public int categoryId { get; set; }
    public string categoryName { get; set; }
    public string description { get; set; }
    public string[] products { get; set; }
}
