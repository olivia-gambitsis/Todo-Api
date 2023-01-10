namespace TodoApi.Services
{
    public class TodoService
    {
        private readonly string? _myString;

        public TodoService()
        {
        }

        public TodoService(string myString)
        {
            _myString = myString;
        }

        public string MyMethod()
        {
            return _myString;
        }
    }
}
