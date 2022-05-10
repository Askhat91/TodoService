using Business_Layer.DTO;
using Business_Layer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoServices _todoServices;

        public TodoItemsController( ITodoServices todoServices)
        {
            _todoServices = todoServices;
        }

        [HttpGet("GetTodoItems")]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems() => Ok(await _todoServices.GetTodoItems());

        [HttpGet("GetTodoItem/{id}")]
        [ActionName(nameof(GetTodoItem))]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            var todoItem = await _todoServices.GetTodoItem(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        [HttpPut("UpdateTodoItem/{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                return BadRequest();
            }

            var todoItem = await _todoServices.GetTodoItem(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            try
            {
                await _todoServices.UpdateTodoItem(todoItemDTO);
            }
            catch (DbUpdateConcurrencyException) when (!_todoServices.TodoItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost("CreateTodoItem")]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var result = await _todoServices.CreateTodoItem(todoItemDTO);
            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = result.Id }, result);
        }

        [HttpDelete("DeleteTodoItem/{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _todoServices.GetTodoItem(id);

            if (todoItem == null)
            {
                return NotFound();
            }
            await _todoServices.DeleteTodoItem(id);

            return NoContent();
        }  
    }
}
