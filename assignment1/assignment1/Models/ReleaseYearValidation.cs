using System.ComponentModel.DataAnnotations;

namespace assignment1.Models
{
    public class ReleaseYearValidation : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            var year = Convert.ToInt64(value);
            return year <= DateTime.Now.Year - 3;
        }
    }
}
