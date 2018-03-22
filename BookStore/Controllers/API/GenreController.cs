using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers.API
{
    [Route("api/[controller]")]
    public class GenreController : Controller
    {
        private readonly IGenreService genreService;

        public GenreController(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> Find()
        {
            return new ObjectResult(await genreService.FindAllAsync());
        }

        [HttpGet("{id}", Name = "FindGenreById")]
        public async Task<IActionResult> FindById(int id)
        {
            var item = await genreService.FindByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        [HttpPost]
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> Create([Bind("Name")] Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Genre newGenre = await genreService.CreateAsync(genre);
            if (newGenre == null)
            {
                return BadRequest();
            }

            return CreatedAtRoute("FindGenreById", new { id = newGenre.Id }, newGenre);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Name")] Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Genre newGenre = await genreService.UpdateAsync(id, genre);

            if (newGenre == null)
            {
                return NotFound();
            }

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await genreService.DeleteAsync(id))
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
