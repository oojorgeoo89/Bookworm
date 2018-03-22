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
    public class LanguageController : Controller
    {
        private readonly ILanguageService languageService;

        public LanguageController(ILanguageService languageService)
        {
            this.languageService = languageService;
        }

        [HttpGet]
        public async Task<IActionResult> Find()
        {
            return new ObjectResult(await languageService.FindAllAsync());
        }

        [HttpGet("{id}", Name = "FindLanguageById")]
        public async Task<IActionResult> FindById(int id)
        {
            var item = await languageService.FindByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        [HttpPost]
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> Create([Bind("Name")] Language language)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Language newLanguage = await languageService.CreateAsync(language);
            if (newLanguage == null)
            {
                return BadRequest();
            }

            return CreatedAtRoute("FindLanguageById", new { id = newLanguage.Id }, newLanguage);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Name")] Language language)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Language newLanguage = await languageService.UpdateAsync(id, language);

            if (newLanguage == null)
            {
                return NotFound();
            }

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await languageService.DeleteAsync(id))
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
