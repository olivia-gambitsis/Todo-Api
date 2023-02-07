using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;



namespace TodoApi.Models;

//TodoContext inherits from DbContext base class
public class TodoContext : DbContext
{
    // Pass DbContextOptions object to our DBContext base. The Options parameter is set as UseInMemoryDatabase in the main program. 
    public TodoContext(DbContextOptions<TodoContext> options)
        : base(options) { }

    //Declare a collection of Todos to represent rows of todo items in our database using DbSet 
    public DbSet<TodoItem> TodoItems { get; set; } = null!;
}

