using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoItem>> GetTodoItems();
        Task<TodoItem> GetTodoItem(long id);
        Task UpdateTodoItem(TodoItem todoItem);
        Task<TodoItem> CreateTodoItem(TodoItem todoItem);
        Task DeleteTodoItem(long id);
        bool TodoItemExists(long id);
    }
}
