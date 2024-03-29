﻿using Microsoft.AspNetCore.Mvc;
using ReactToDoTask.Server.Models;
using ReactToDoTask.Server.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReactToDoTask.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private readonly ILogger<TodoItemController> _logger;
        private readonly ITodoService _todoService;

        public TodoItemController(ILogger<TodoItemController> logger,ITodoService todoService)
        {
            _logger = logger;
            _todoService = todoService;
        }
       
        // GET: api/<TodoItemController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_todoService.GetAll());
        }

        // GET api/<TodoItemController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if(id <1)
                return BadRequest();
            var item = _todoService.GetById(id);
            if (item==null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        // POST api/<TodoItemController>
        [HttpPost]
        public IActionResult Post([FromBody] TodoItem value)
        {
            if(value == null)
                return BadRequest();

            return Ok(_todoService.Insert(value));
        }

        // PUT api/<TodoItemController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] TodoItem value)
        {
            if (id<1 || value==null)
            {
                return BadRequest();
            }

            value.Id = id;            
            return Ok(_todoService.Update(value));
        }

        // DELETE api/<TodoItemController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id<1)
            {
                return BadRequest();
            }
            _todoService.Delete(id);
            return Ok();
        }
    }
}
