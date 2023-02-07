
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using InMemoryDbAspNet6WebAPI;

namespace TodoApi.Controllers
{
    //"api/TodoItems" = route template this string becomes part of our desired URI e.g.https://localhost:7124/api/todoitems
    [Route("api/TodoItems")]

    //Applies inference rules for the default data sources of action parameters. -A binding source attribute defines the location at which an action parameter's value is found [FromBody] etc
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoRepository _todoRepository;

        public TodosController(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        // GET: api/TodoItems - [HttpGet] create GET endpoint for the clitent to send a GET request
        //REST APIs should use attribute routing to model the app’s functionality as a set of resources where operations are represented by HTTP verbs.
        //Attribute routing uses a set of attributes to map actions directly to route templates., if the incomming request is GET we know to rout it to this method :)

        [HttpGet]
        public async Task<IEnumerable<TodoItem>> GetTodoItems()
        {

            return await _todoRepository.GetTodosAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoItem([FromRoute] long id)
        {
            var todoItem = await _todoRepository.GetTodoByIdAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return Ok(todoItem);
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)
        {
            if(id != todoItem.Id)
            {
                return BadRequest();
            }
            var updatedTodo = await _todoRepository.UpdateTodoAsync(id, todoItem);
            if(updatedTodo == null)
            {
                return NotFound();
            }
            return Ok(updatedTodo);
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            await _todoRepository.AddTodoAsync(todoItem);

            //CreatedAtAction(Action Name, Route Values, Created Resource) - allows us to return a resource location e.g.https://localhost:7124/api/todoitems/1
            // Action Name is the method name
            // Route Value is newly created todo.id
            // The third param can be anything but here we return the newly created employee object
            return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todo = await _todoRepository.DeleteTodoAsync(id);
            if(todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchTodoItem([FromRoute] long id, [FromBody] JsonPatchDocument patchEntity)
        {
            var updatedTodo = await _todoRepository.UpdateTodoPatchAsync(id, patchEntity);
            if(updatedTodo == null)
            {
                return NotFound();
            }
            return Ok(updatedTodo);

        }

    }


    
}
