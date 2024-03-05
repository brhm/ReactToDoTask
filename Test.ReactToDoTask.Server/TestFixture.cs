using ReactToDoTask.Server.Models;
using ReactToDoTask.Server.Repositories;
using ReactToDoTask.Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.ReactToDoTask.Server
{
    public class TestFixture : IDisposable
    {
        public IRepository<TodoItem> TodoRepository { get; private set; }
        public ITodoService TodoService { get; private set; }

        public TestFixture()
        {
            // IRepository and ITodoService setup
            TodoRepository = new TodoItemRepository(); 
            TodoService = new TodoService(TodoRepository); 
        }

        public void Dispose()
        {
            // Gerektiğinde temizlik işlemleri burada yapılabilir
        }
    }
}
