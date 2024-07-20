using Microsoft.EntityFrameworkCore;
using TutorialRestApi.Todo.App.Models;
using TutorialRestApi.Todo.App.Exceptions;
using TutorialRestApi.Todo.App.Repositories;
using TutorialRestApi.Core.Database;

namespace TutorialRestApi.Todo.Infrastructure.Database
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _context;
        public TodoRepository(TodoContext context) 
        {
            _context = context;
        }

        public IEnumerable<TodoItem> FetchAll()
        {
            return _context.TodoItems.Include(x => x.User).ToList();
        }

        public TodoItem? FetchById(long id) 
        {          
            return _context
                .TodoItems
                .Include(x => x.User)
                .Where(t => t.Id == id)
                .FirstOrDefault();
        }

        public TodoItem Create(TodoItem item) 
        {
            _context.TodoItems.Add(item);
            _context.SaveChanges();

            return item;
        }

        public void Update(TodoItem item)
        {
            try { 
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            } catch (DbUpdateConcurrencyException)
            {
                if (_context.TodoItems.Find(item.Id) == null)
                {
                    throw new TodoItemNotFound();
                } else
                {
                    throw;
                }
            }
        }

        public void Delete(long Id)
        {
            TodoItem? item = _context.TodoItems.Find(Id);

            if (item == null) {
                throw new TodoItemNotFound();
            }

            _context.TodoItems.Remove(item);
            _context.SaveChanges();
        }
    }
}
