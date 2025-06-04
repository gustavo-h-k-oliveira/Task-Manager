using System.ComponentModel.DataAnnotations;

public class UpdateTaskItemDto
{
    [Required(ErrorMessage = "O título é obrigatório.")]
    [StringLength(100, ErrorMessage = "O título não pode exceder 100 caracteres.")]
    public string Title { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres.")]
    public string Description { get; set; } = string.Empty;

    public bool IsCompleted { get; set; }
}
