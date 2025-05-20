using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace assignment4.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = null!;
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = null!;

    public string? Title { get; set; }
    [Display(Name = "Title of Courtesy")]
    public string? TitleOfCourtesy { get; set; }
    [Display(Name = "Birth Date")]
    [DataType(DataType.Date)]
    public DateTime? BirthDate { get; set; }
    [Display(Name = "Hire Date")]
    [DataType(DataType.Date)]
    public DateTime? HireDate { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Region { get; set; }
    [Display(Name = "Postal Code")]
    public string? PostalCode { get; set; }

    public string? Country { get; set; }
    [Display(Name = "Home Phone")]
    public string? HomePhone { get; set; }

    public string? Extension { get; set; }

    public string? Notes { get; set; }
    [Display(Name = "Reports To")]
    public int? ReportsTo { get; set; }
    [Display(Name = "Photo")]
    public string? PhotoPath { get; set; }

    public virtual ICollection<Employee> InverseReportsToNavigation { get; set; } = new List<Employee>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Employee? ReportsToNavigation { get; set; }
}
