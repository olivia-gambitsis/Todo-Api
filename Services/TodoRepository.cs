using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.JsonPatch;
using TodoApi.Models;

namespace InMemoryDbAspNet6WebAPI;
public class TodoRepository : ITodoRepository
{
    private readonly TodoContext _dbContext;

    public TodoRepository(TodoContext dBContext)
    {
        _dbContext = dBContext;
    }

    public async Task<IEnumerable<TodoItem>> GetTodosAsync()
    {
        //Because this returns a collection from a database -> I have to convert the result to a list, thus the call to .ToListAsync().
        var query = (from todoItems in _dbContext.TodoItems
                     select todoItems).ToListAsync();
        return await query;
    }

    public async Task<TodoItem> GetTodoByIdAsync(long id)
    {
        //FindAsync() comes from DbSet Class 
        TodoItem todo = await _dbContext.TodoItems.FindAsync(id);
        return todo;

    }

    public async Task<TodoItem> AddTodoAsync(TodoItem todo)
    {
        //Add() method from DbSet class, we return the todo object in the response so the client can use it.
        _dbContext.TodoItems.Add(todo);
        await _dbContext.SaveChangesAsync();

        return todo;
    }

    public async Task<TodoItem> DeleteTodoAsync(long id)
    {
        var todo = await GetTodoByIdAsync(id);
        if (todo == null)
        {
            //this variable will be null so this returns null.
            return todo;
        }
        _dbContext.TodoItems.Remove(todo);
        await _dbContext.SaveChangesAsync();

        return todo;

    }

    //Full record update
    public async Task<TodoItem> UpdateTodoAsync(long id, TodoItem todo)
    {
        var todoQuery = await GetTodoByIdAsync(id);
        if (todoQuery == null)
        {
            return todoQuery;
        }
        //set the value of the current employee object to the new one.
        _dbContext.Entry(todoQuery).CurrentValues.SetValues(todo);
        await _dbContext.SaveChangesAsync();

        return todoQuery;
    }

    //Can be used for partial record update 
    public async Task<TodoItem> UpdateTodoPatchAsync(long id, JsonPatchDocument todoDocument)
    {
        var todoQuery = await GetTodoByIdAsync(id);
        if (todoQuery == null)
        {
            return todoQuery;
        }
        todoDocument.ApplyTo(todoQuery);
        await _dbContext.SaveChangesAsync();

        return todoQuery;
    }
}