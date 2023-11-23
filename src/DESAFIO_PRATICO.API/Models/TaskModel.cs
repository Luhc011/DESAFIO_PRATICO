using DESAFIO_PRATICO.API.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DESAFIO_PRATICO.API.Models
{
    public class TaskModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Title field is required.")]
        [StringLength(100, ErrorMessage = "The Title field cannot exceed 100 characters.")]
        public string? Title { get; set; }

        [StringLength(500, ErrorMessage = "The Description field cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "The Status field is required.")]
        public StatusTasks Status { get; set; }

        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public UserModel? User { get; set; } 

    }
}
