using System.ComponentModel.DataAnnotations;

namespace assignment1.Models
{
    public class GenreValidation : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            var genre = Convert.ToString(value);
            if (genre == "Action" || genre == "Driving" || genre == "FPS")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
