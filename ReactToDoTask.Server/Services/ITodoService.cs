using ReactToDoTask.Server.Models;

namespace ReactToDoTask.Server.Services
{
    public interface ITodoService
    {
        List<TodoItem> GetAll();
        TodoItem GetById(int id);
        TodoItem Insert(TodoItem item);
        TodoItem Update(TodoItem item);
        void Delete(int id);
    }
}
