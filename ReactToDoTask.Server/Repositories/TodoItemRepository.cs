using Microsoft.Extensions.Caching.Memory;
using ReactToDoTask.Server.Models;

namespace ReactToDoTask.Server.Repositories
{
    public class TodoItemRepository : IRepository<TodoItem>
    {
        private readonly IMemoryCache _memoryCache;
        public List<TodoItem> TodoItemList;

        public TodoItemRepository(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            TodoItemList = new List<TodoItem>();

            _memoryCache.TryGetValue<List<TodoItem>>("TodoItemList", out TodoItemList);
            if (TodoItemList == null)
            {
                TodoItemList = new()
                {
                    new TodoItem {Id=1,Title="React js Lernen",Completed=false},
                    new TodoItem {Id=2,Title="SQL Lernen",Completed= true},
                    new TodoItem {Id=3,Title="Angular js Lernen",Completed = true},
                    new TodoItem {Id=4,Title="Azure Devops Lernen",Completed = false},
                    new TodoItem {Id=5,Title=".Net Core Lernen",Completed = false},
                    new TodoItem {Id=6,Title="Data Science Lernen",Completed = true},
                    new TodoItem {Id=7,Title="SOLID Lernen",Completed = true},
                    new TodoItem {Id=8,Title="DDD Architecture Lernen",  Completed = false},
                    new TodoItem {Id=9,Title="Javascript Lernen",Completed = true}
                };
                _memoryCache.Set("TodoItemList",TodoItemList);
            }
        
        }
    
        public void Delete(int id)
        {
            var hasTodoItem = TodoItemList.Find(p => p.Id == id);

            if (hasTodoItem == null)
            {

                throw new Exception($"{id} - Todoitem is not found");

            }

            TodoItemList.Remove(hasTodoItem);
        }

        public List<TodoItem> GetAll()
        {
            return TodoItemList;
        }

        public TodoItem GetById(int id)
        {
            var hasTodoItem = TodoItemList.Find(p => p.Id == id);

            if (hasTodoItem == null)
            {

                throw new Exception($"{id} - Todoitem is not found");

            }

            return hasTodoItem;
        }

        public void Insert(TodoItem item)
        {
            var hasProduct = TodoItemList.Any(p => p.Id == item.Id);

            if (!hasProduct)
            {
                item.Id=TodoItemList.Max(p => p.Id)+1;

                TodoItemList.Add(item);
                _memoryCache.Set("TodoItemList", TodoItemList);
            }
        }

        public void Update(TodoItem item)
        {
            var hasProduct = TodoItemList.Any(p => p.Id == item.Id);
            var index = TodoItemList.FindIndex(p => p.Id == item.Id);

            TodoItemList[index] = item;
            _memoryCache.Set("TodoItemList", TodoItemList);

        }
    }
}
