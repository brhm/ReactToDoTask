using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using ReactToDoTask.Server.Controllers;
using ReactToDoTask.Server.Models;
using ReactToDoTask.Server.Repositories;
using ReactToDoTask.Server.Services;
using System.Security.Cryptography;

namespace Test.ReactToDoTask.Server
{
    public class TodoItemControllerTests: IClassFixture<TestFixture>
    {
        private readonly TestFixture _fixture;

        private TodoItemController _controller;
        //ITodoService _service;
        //IRepository<TodoItem> _repository;
        public TodoItemControllerTests(TestFixture fixture)
        {
            _fixture = fixture;
            _controller = new TodoItemController(new NullLogger<TodoItemController>(), _fixture.TodoService);
        }
       
        [Fact]
        public void Get_ReturnsTodoItemList_Success()
        {
            // Arrange
            
            // Act
            var result = _controller.Get();
            var resultType= result as OkObjectResult;
            var resultList= resultType?.Value as List<TodoItem>;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<TodoItem>>(resultType?.Value);
            Assert.True(resultList?.Any());

        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetById_ReturnsTodoItem_Success(int id)
        {
            // Arrange
            var _id = id;
            // Act
            var result = _controller.Get(id);
            var resultType = result as OkObjectResult;
            var todoItem = resultType?.Value as TodoItem;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<TodoItem>(resultType?.Value);
            Assert.Equal(_id, todoItem?.Id);

        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-2)]
        [InlineData(-3)]
        public void GetById_ReturnsBadRequest(int id)
        {
            // Arrange
            var _id = id;
            // Act
            var result = _controller.Get(_id);
            var resultType = result as BadRequestResult;
           
            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(resultType);
        }

        [Fact]
        public void Post_ReturnNewTodoItem_Success()
        {
            // Arrange
            TodoItem item =new TodoItem(){Id=0,Completed=false,Title="Test Tasks",DeadlineDate=DateTime.Now.AddDays(1)};

            // Act
            var result = _controller.Post(item);
            var resultType = result as OkObjectResult;
            var todoItem = resultType?.Value as TodoItem;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<TodoItem>(resultType?.Value);
            Assert.NotEqual(0, todoItem?.Id);
        }

        [Fact]
        public void Post_ReturnTodoItem_NoInsert()
        {
            // Arrange
            TodoItem item = new TodoItem() { Id = 1, Completed = false, Title = "Test Tasks", DeadlineDate = DateTime.Now.AddDays(1) };

            // Act
            var result = _controller.Post(item);
            var resultType = result as OkObjectResult;
            var todoItem = resultType?.Value as TodoItem;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<TodoItem>(resultType?.Value);
            Assert.Equal(item.Id, todoItem?.Id);
        }
        [Fact]
        public void Post_SendNull_ReturnBadRequest()
        {
            // Arrange
            TodoItem item=null;

            // Act
            var result = _controller.Post(item);
            var resultType = result as BadRequestResult;

            // Assert
            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(resultType);
        }

        [Fact]
        public void Put_UpdateItem_Success()
        {
            // Arrange
            var id = 2;
            var updateItemResut = _controller.Get(id);
            var updateItemType = updateItemResut as OkObjectResult;
            var updateItem = updateItemType?.Value as TodoItem;

            updateItem.Title = "Test Update";
            updateItem.Completed = true;


            // Act
            var result = _controller.Put(id,updateItem);
            var resultType = result as OkObjectResult;
            var todoItem = resultType?.Value as TodoItem;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<TodoItem>(resultType?.Value);
            Assert.Equal(updateItem.Title, todoItem?.Title);
            Assert.Equal(updateItem.Id, todoItem?.Id);
            Assert.Equal(updateItem.Completed, todoItem?.Completed);
        }

        [Fact]
        public void Put_SendIdZero_ReturnBadRequest()
        {
            // Arrange
            var id = 2;
            var updateItemResut = _controller.Get(id);
            var updateItemType = updateItemResut as OkObjectResult;
            var updateItem = updateItemType?.Value as TodoItem;

            updateItem.Title = "Test Update";
            updateItem.Completed = true;

            // Act
            var result = _controller.Put(0,updateItem);
            var resultType = result as BadRequestResult;

            // Assert
            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(resultType);
        }
        [Fact]
        public void Put_SendTodoItemNull_ReturnBadRequest()
        {
            // Arrange
            var id = 2;
            TodoItem updateItem = null;
            // Act
            var result = _controller.Put(id, updateItem);
            var resultType = result as BadRequestResult;

            // Assert
            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(resultType);
        }

        [Fact]
        public void Delete_Item_Success()
        {
            // Arrange
            var id = 5;

            // Act
            var result = _controller.Delete(id);
            var resultType = result as OkResult;

            var getTodoItemResult=_controller.Get(id);
            var getTodoItemResultType = getTodoItemResult as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200,resultType?.StatusCode);
            Assert.Equal(404, getTodoItemResultType?.StatusCode);
        }
        [Fact]
        public void Delete_ReturnBadRequest()
        {
            // Arrange
            var id = 0;
            // Act
            var result = _controller.Delete(0);
            var resultType = result as BadRequestResult;

            // Assert
            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(resultType);
        }
    }
}