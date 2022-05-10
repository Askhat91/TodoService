using Business_Layer.DTO;
using Business_Layer.Helpers;
using Business_Layer.Interfaces;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services
{ 
    public class TodoServices : ITodoServices
    {
        private readonly ITodoRepository _todoRepository;

        public TodoServices(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }
        public async Task<IEnumerable<TodoItemDTO>> GetTodoItems()
        {
            var result = await _todoRepository.GetTodoItems();
            return result.Select(x => MapperHelper.ItemToDTO(x)).ToList();
        }
        public async Task<TodoItemDTO> GetTodoItem(long id) => MapperHelper.ItemToDTO(await _todoRepository.GetTodoItem(id));

        public async Task UpdateTodoItem(TodoItemDTO todoItemDTO)
        {
            await _todoRepository.UpdateTodoItem(MapperHelper.DtoTOItem(todoItemDTO));
        }

        public async Task<TodoItemDTO> CreateTodoItem(TodoItemDTO todoItemDTO) 
        {

            var result = await _todoRepository.CreateTodoItem(MapperHelper.DtoTOItem(todoItemDTO));
            return MapperHelper.ItemToDTO(result);
        }
        public async Task DeleteTodoItem(long id)
        {
            await _todoRepository.DeleteTodoItem(id);
        }

        public bool TodoItemExists(long id) =>
             _todoRepository.TodoItemExists(id);


    }
}
