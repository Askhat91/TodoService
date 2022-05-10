using Business_Layer.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Interfaces
{
    public interface ITodoServices
    {
        Task<IEnumerable<TodoItemDTO>> GetTodoItems();
        Task<TodoItemDTO> GetTodoItem(long id);
        Task UpdateTodoItem(TodoItemDTO todoItemDTO);
        Task<TodoItemDTO> CreateTodoItem(TodoItemDTO todoItemDTO);
        Task DeleteTodoItem(long id);
        bool TodoItemExists(long id);
    }
}
