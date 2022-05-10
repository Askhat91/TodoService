using Business_Layer.DTO;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Helpers
{
    public static class MapperHelper
    {
        public static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
            todoItem is null? null: new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
        public static TodoItem DtoTOItem(TodoItemDTO todoItemDTO) =>
            todoItemDTO is null ? null:new TodoItem
            {
                Id = todoItemDTO.Id,
                Name = todoItemDTO.Name,
                IsComplete = todoItemDTO.IsComplete
            };
    }
}
