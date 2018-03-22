using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Models.Helpers;
using BookStore.Models.ViewModels;
using BookStore.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers.API
{
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private readonly IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> Find(PageInfo pageInfo, BookFilters filters)
        {
            ICollection<Book> foundItems;

            if (filters == null)
                foundItems = await bookService.FindAllAsync(pageInfo);
            else
                foundItems = await bookService.SearchAsync(filters, pageInfo);

            return new ObjectResult(foundItems);
        }

        [HttpGet("{id}", Name = "FindById")]
        public async Task<IActionResult> FindById(int id)
        {
            foreach (var claim in User.Claims) 
            {
                Console.Write(claim);
            }
            var item = await bookService.FindByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        [HttpPost]
        [Authorize( Policy = "admin")]
        public async Task<IActionResult> Create([Bind("Title", "Summary", "Isbn", "Image")] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Book newBook = await bookService.CreateAsync(book);
            if (newBook == null)
            {
                return BadRequest();
            }

            return CreatedAtRoute("FindById", new { id = newBook.Id }, newBook);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Title", "Summary", "Isbn", "Image")] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Book newBook = await bookService.UpdateAsync(id, book);

            if (newBook == null)
            {
                return NotFound();
            }

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await bookService.DeleteAsync(id))
            {
                return new NoContentResult();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
