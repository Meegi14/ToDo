﻿
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ToDo.Models;

namespace ToDo.Controllers
{
    [Produces("application/json")]
    [Route("api/todo")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoContext _context;

        public ToDoController(ToDoContext context)
        {
            _context = context;

            if (_context.ToDoItem.Count() == 0)
            {
               
                _context.ToDoItem.Add(new ToDoItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }
     
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetToDoItems()
        {
            return await _context.ToDoItem.ToListAsync();
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItem>> GetToDoItem(long id)
        {
            var todoItem = await _context.ToDoItem.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }
        /// <summary>
        /// Creates a ToDoItem.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// Post /ToDo
        /// {
        /// "id":1,
        /// "name": "Item1",
        /// "isComplete": true
        /// }
        /// </remarks>
        /// <param name="todoItem"></param>
        /// <returns>A newly created ToDoItem
        /// </returns>
        /// <response code="201">Returns the newly created item </response>
        /// <response code="400"> If the item is null</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ToDoItem>> PostTodoItem(ToDoItem todoItem)
        {
            _context.ToDoItem.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetToDoItem", new { id = todoItem.id }, todoItem);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, ToDoItem todoItem)
        {
            if (id != todoItem.id)
            {
                return BadRequest();
            }

            _context.Entry(todoItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
 ///<summary>
 ///Deletes a specific ToDoItem.
 /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ToDoItem>> DeleteTodoItem(long id)
        {
            var todoItem = await _context.ToDoItem.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.ToDoItem.Remove(todoItem);
            await _context.SaveChangesAsync();

            return todoItem;
        }
    }
}

