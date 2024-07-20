using TutorialRestApi.Todo.App.Models;

namespace TutorialRestApi.Todo.App.Repositories
{
    public interface ITodoRepository
    {
        public IEnumerable<TodoItem> FetchAll();

        public TodoItem? FetchById(long id);

        public TodoItem Create(TodoItem item);
        public void Update(TodoItem item);

        public void Delete(long id);
    }
}
