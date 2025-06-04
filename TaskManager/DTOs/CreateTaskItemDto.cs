using System.ComponentModel.DataAnnotations;

namespace TaskManager.DTOs
{
    public class CreateTaskItemDto
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(100, ErrorMessage = "O título não pode exceder 100 caracteres.")]
        public string Title { get; set; } = string.Empty;
        [StringLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres.")]
        public string Description { get; set; } = string.Empty;
    }
}
