using System.ComponentModel.DataAnnotations;

namespace assignment1.Models
{
    public class Game
    {
        [Key]
        public int GameId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Developer { get; set; }
        [Required]
        [GenreValidation(ErrorMessage = "Genre must be Action, Driving, or FPS")]
        public string Genre { get; set; }
        [Required]
        [Display(Name = "Release Year")]
        [ReleaseYearValidation(ErrorMessage = "Must be at least 3 years old")]
        public int ReleaseYear { get; set; }
        [Display(Name = "Purchase Date")]
        [DataType(DataType.Date)]
        [PurchaseDateValidation(ErrorMessage = "Purchase Date cannot be in the future")]
        public DateTime? PurchaseDate { get; set; }
        [Range(1,100)]
        public int? Rating {  get; set; }
    }
}
