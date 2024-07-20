using System.ComponentModel.DataAnnotations;

namespace TutorialRestApi.Todo.Infrastructure.Dtos
{
    public class EditTodoItemDTO
    {
        [Required(AllowEmptyStrings = false)]
        public string? Name { get; set; }

        [Required]
        public bool IsComplete { get; set; }
    }
}
