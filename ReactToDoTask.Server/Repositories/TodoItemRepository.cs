using Microsoft.Extensions.Caching.Memory;
using ReactToDoTask.Server.Data;
using ReactToDoTask.Server.Models;
using System;

namespace ReactToDoTask.Server.Repositories
{
    public class TodoItemRepository : IRepository<TodoItem>
    {
        //private readonly IMemoryCache _memoryCache;
       
        public TodoItemRepository()
        {


            using (var context = new MyDbContext())
            {
                var todoItemList = new List<TodoItem>
                {
                    new TodoItem {Id=1,Title="React js Lernen",CreatedDate=DateTime.Now.AddDays(-5), DeadlineDate=DateTime.Now.AddDays(3), Completed=false},
                    new TodoItem {Id=2,Title="SQL Lernen",CreatedDate=DateTime.Now.AddDays(-5), DeadlineDate = DateTime.Now.AddDays(3),Completed= true},
                    new TodoItem {Id = 3, Title = "Angular js Lernen", CreatedDate = DateTime.Now.AddDays(-3), DeadlineDate = DateTime.Now.AddDays(3), Completed = true},
                    new TodoItem {Id = 4, Title = "Azure Devops Lernen", CreatedDate = DateTime.Now.AddDays(-5), DeadlineDate = DateTime.Now.AddDays(-1), Completed = false},
                    new TodoItem {Id = 5, Title = ".Net Core Lernen", CreatedDate = DateTime.Now.AddDays(-5), DeadlineDate = DateTime.Now.AddDays(3), Completed = false},
                    new TodoItem {Id = 6, Title = "Data Science Lernen", CreatedDate = DateTime.Now.AddDays(-5), DeadlineDate = DateTime.Now.AddDays(3), Completed = true},
                    new TodoItem {Id = 7, Title = "SOLID Lernen", CreatedDate = DateTime.Now.AddDays(-5), DeadlineDate = DateTime.Now.AddDays(3), Completed = true},
                    new TodoItem {Id = 8, Title = "DDD Architecture Lernen", CreatedDate = DateTime.Now.AddDays(-5), DeadlineDate = DateTime.Now.AddDays(-2), Completed = false},
                    new TodoItem {Id = 9, Title = "Javascript Lernen", CreatedDate = DateTime.Now.AddDays(-5), DeadlineDate = DateTime.Now.AddDays(3), Completed = true}
                };

                context.TodoItems.AddRange(todoItemList);
                context.SaveChanges();

            }



        
        }
    
        public void Delete(int id)
        {

            using (var context = new MyDbContext())
            {

                var hasTodoItem = context.TodoItems.FirstOrDefault(p => p.Id == id);

                if (hasTodoItem == null)
                {

                    throw new Exception($"{id} - Todoitem is not found");

                }

                context.TodoItems.Remove(hasTodoItem);
                context.SaveChanges();
            }
        }

        public List<TodoItem> GetAll()
        {
            using (var context = new MyDbContext())
            {

                return context.TodoItems.OrderBy(p=>p.Id).ToList();
            }
        }

        public TodoItem GetById(int id)
        {
            using (var context = new MyDbContext())
            {

                var hasTodoItem = context.TodoItems.FirstOrDefault(p => p.Id == id);

                return hasTodoItem;
            }
        }

        public TodoItem Insert(TodoItem item)
        {
            using (var context = new MyDbContext())
            {

                var hasProduct = context.TodoItems.Any(p => p.Id == item.Id);

                if (!hasProduct)
                {
                    item.Id = context.TodoItems.Max(p => p.Id) + 1;

                    context.TodoItems.Add(item);
                    context.SaveChanges();
                }
            }
            return item;
        }

        public TodoItem Update(TodoItem item)
        {
            using (var context = new MyDbContext())
            {

                var hasProduct = context.TodoItems.FirstOrDefault(p => p.Id == item.Id);
                if (hasProduct != null)
                {
                    hasProduct.Title = item.Title;
                    hasProduct.Completed=item.Completed;
                    hasProduct.DeadlineDate=item.DeadlineDate;
                    hasProduct.CompletedDate = DateTime.Now;
                    //context.TodoItems.Update(item);
                    context.SaveChanges();
                    return hasProduct;
                }
            }
            return item;
        }
    }
}
