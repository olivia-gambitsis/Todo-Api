using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace InMemoryDbAspNet6WebAPI;

public interface ITodoRepository
{
    Task<IEnumerable<TodoItem>> GetTodosAsync();

    Task<TodoItem> GetTodoByIdAsync(long id);

    Task<TodoItem> AddTodoAsync(TodoItem todo);

    Task<TodoItem> DeleteTodoAsync(long id);

    Task<TodoItem> UpdateTodoAsync(long id, TodoItem todo);

    Task<TodoItem> UpdateTodoPatchAsync(long id, JsonPatchDocument todo);

}

