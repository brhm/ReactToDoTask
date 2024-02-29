using ReactToDoTask.Server.Models;

namespace ReactToDoTask.Server.Services
{
    public interface ITodoService
    {
        List<TodoItem> GetAll();
        TodoItem GetById(int id);
        void Insert(TodoItem item);

        void Update(TodoItem item);
        void Delete(int id);
    }
}
