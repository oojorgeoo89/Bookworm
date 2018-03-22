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
    public class FormatController : Controller
    {
        private readonly IFormatService formatService;

        public FormatController(IFormatService formatService)
        {
            this.formatService = formatService;
        }

        [HttpGet]
        public async Task<IActionResult> Find()
        {
            return new ObjectResult(await formatService.FindAllAsync());
        }

        [HttpGet("{id}", Name = "FindFormatById")]
        public async Task<IActionResult> FindById(int id)
        {
            var item = await formatService.FindByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        [HttpPost]
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> Create([Bind("Name")] Format format)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Format newFormat = await formatService.CreateAsync(format);
            if (newFormat == null)
            {
                return BadRequest();
            }

            return CreatedAtRoute("FindFormatById", new { id = newFormat.Id }, newFormat);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Name")] Format format)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Format newFormat = await formatService.UpdateAsync(id, format);

            if (newFormat == null)
            {
                return NotFound();
            }

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await formatService.DeleteAsync(id))
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
