using Microsoft.EntityFrameworkCore;
using ReactToDoTask.Server.Models;
using System;

namespace ReactToDoTask.Server.Data
{
    public class MyDbContext:DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "TodoListDB");
        }
     
    }
}
