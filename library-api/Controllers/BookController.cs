using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using library_api.Entities;
using library_api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace library_api.Controllers
{
    public class BookController : Controller
    {
        private readonly IBook _book;

        public BookController(IBook book)
        {
            _book = book;
        }

        [Route("books")]
        [HttpGet]
        [Authorize]
        public IActionResult List()
        {
            try
            {
                return Ok(_book.List());
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [Route("book/{bookID}")]
        [HttpGet]
        [Authorize]
        public IActionResult Get(int bookID)
        {
            try
            {
                return Ok(_book.Get(bookID));
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [Route("book")]
        [HttpPost]
        [Authorize]
        public IActionResult Create([FromBody] Book book)
        {
            try
            {
                return Ok(_book.Create(book));
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [Route("book/{bookID}")]
        [HttpPut]
        [Authorize]
        public IActionResult Update(int bookID, [FromBody] Book book)
        {
            try
            {
                book.Id = bookID;
                return Ok(_book.Update(book));
            } catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [Route("book/{bookID}")]
        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int bookID)
        {
            try
            {
                Book book = _book.Get(bookID);
                return Ok(book.Delete(book));
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }

        }
    }
}