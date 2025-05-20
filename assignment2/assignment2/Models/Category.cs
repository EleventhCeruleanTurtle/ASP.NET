using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace assignment2.Models;

[Table("categories")]
[Index("CategoryName", Name = "category_name")]
public partial class Category
{
    [Key]
    [Column("category_id")]
    public int CategoryId { get; set; }

    [Column("category_name")]
    [StringLength(15)]
    [Display(Name = "Name")]
    public string CategoryName { get; set; } = null!;

    [Column("description")]
    [StringLength(75)]
    [Display(Name = "Description")]
    public string? Description { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
