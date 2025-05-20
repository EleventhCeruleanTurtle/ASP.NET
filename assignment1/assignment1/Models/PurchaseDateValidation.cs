using System.ComponentModel.DataAnnotations;

namespace assignment1.Models
{
    public class PurchaseDateValidation : ValidationAttribute
    {
        public override bool IsValid(object? value) 
        {
            var dateSeen = Convert.ToDateTime(value);
            return dateSeen < DateTime.Now;
        }
    }
}
