namespace TodoApi.Services
{
    public interface ITodoService
    {
        string GetTodo();
        List<string> GetTodos();
    }
}
