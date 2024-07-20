using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using TutorialRestApi.Todo.App.Exceptions;
using TutorialRestApi.Todo.App.Models;
using TutorialRestApi.Todo.App.Repositories;
using TutorialRestApi.Todo.Infrastructure.Dtos;
using TutorialRestApi.User.App.Models;
using TutorialRestApi.User.Infrastructure.Helper;

namespace TutorialRestApi.Todo.Infrastructure.Controllers
{
    
    [Route("api/todoitems")]
    [ApiController]
    [Authorize]
    public class TodoItemsController : ControllerBase
    {
        // GET: api/TodoItems
        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetTodoItems([FromServices] ITodoRepository todoRepository)
        {
            return Ok(todoRepository.FetchAll());
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetTodoItem([FromServices] ITodoRepository repository, long id)
        {
            var todoItem = repository.FetchById(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        // PUT: api/TodoItems/5
        [HttpPut("{id}")]
        public IActionResult PutTodoItem([FromServices] ITodoRepository repository, long id, EditTodoItemDTO todoItemDto, [FromServices] UserResolver userResolver)
        {
            UserModel? currentUser = userResolver.Resolve();


            if (currentUser == null)
            {
                return Unauthorized(); 
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
               repository.Update(new TodoItem {Id = id, Name = todoItemDto.Name, IsComplete = todoItemDto.IsComplete, User = currentUser });
            }
            catch (TodoItemNotFound)
            {
               return NotFound();
            }

            return NoContent();
        }

        // POST: api/TodoItems
        [HttpPost]
        public ActionResult<TodoItem> PostTodoItem([FromServices] ITodoRepository repository, InsertTodoItemDTO todoItemDto, [FromServices] UserResolver userResolver)
        {
            UserModel? currentUser = userResolver.Resolve();

            if (currentUser == null)
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            TodoItem item = repository.Create(new TodoItem { Name = todoItemDto.Name, IsComplete = todoItemDto.IsComplete, User = currentUser });
            return CreatedAtAction("GetTodoItem", new { id = item.Id }, item);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTodoItem([FromServices] ITodoRepository repository, long id)
        {
            try
            {
                repository.Delete(id);
            } catch (TodoItemNotFound)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
