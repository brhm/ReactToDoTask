using Microsoft.Extensions.Caching.Memory;
using NuGet.Protocol.Core.Types;
using ReactToDoTask.Server.Models;
using ReactToDoTask.Server.Repositories;

namespace ReactToDoTask.Server.Services
{
    public class TodoService: ITodoService
    {
        private readonly IRepository<TodoItem> _todoitemRepository;
        public TodoService(IRepository<TodoItem> repository)
        {
            _todoitemRepository=repository;            
        }

        public void Delete(int id)
        {
            _todoitemRepository.Delete(id);
        }

        public List<TodoItem> GetAll()
        {
            return _todoitemRepository.GetAll() ?? new List<TodoItem>();
        }

        public TodoItem GetById(int id)
        {
            return _todoitemRepository.GetById(id);
        }

        public TodoItem Insert(TodoItem item)
        {
           return _todoitemRepository.Insert(item);
        }

        public TodoItem Update(TodoItem item)
        {
            return _todoitemRepository.Update(item);
        }
    }
}
