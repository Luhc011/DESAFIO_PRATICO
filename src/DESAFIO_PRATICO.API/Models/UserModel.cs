using System.ComponentModel.DataAnnotations;

namespace DESAFIO_PRATICO.API.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The field {0} must be between {2} and {1} characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [EmailAddress(ErrorMessage = "The field {0} is in invalid format")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The field {0} must be between {2} and {1} characters")]
        public string? Email { get; set; }
    }
}
