using DataAccessLayer.EF;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _context;

        public TodoRepository(TodoContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TodoItem>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }
        public async Task<TodoItem> GetTodoItem(long id)
        {
            return await _context.TodoItems.FindAsync(id);
        }
        public async Task UpdateTodoItem(TodoItem todoItem)
        {
            var updateToDoItem = await GetTodoItem(todoItem.Id);

            updateToDoItem.Name = todoItem.Name;
            updateToDoItem.IsComplete = todoItem.IsComplete;

            _context.Update(updateToDoItem);
            await _context.SaveChangesAsync();
        }

        public async Task<TodoItem> CreateTodoItem(TodoItem todoItem)
        {
            await _context.TodoItems.AddAsync(todoItem);
            await _context.SaveChangesAsync();
            return todoItem;
        }
        public async Task DeleteTodoItem(long id)
        {
            var deleteTodoItem = await GetTodoItem(id);
            _context.Remove(deleteTodoItem);
            await _context.SaveChangesAsync();
        }
        public bool TodoItemExists(long id) =>
             _context.TodoItems.Any(e => e.Id == id);

    }
}
