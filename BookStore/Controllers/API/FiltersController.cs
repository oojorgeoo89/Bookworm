using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models.ViewModels;
using BookStore.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers.API
{
    
    [Route("api/[controller]")]
    public class FiltersController : Controller
    {

        private IGenreService genreService;
        private IFormatService formatService;
        private ILanguageService languageService;

        public FiltersController(IGenreService genreService,
                              IFormatService formatService,
                              ILanguageService languageService)
        {
            this.genreService = genreService;
            this.formatService = formatService;
            this.languageService = languageService;
        }

        [HttpGet]
        public async Task<IActionResult> findAll()
        {
            AvailableFiltersViewModel model = new AvailableFiltersViewModel();

            var categoriesTask = getCategories(model);
            var formatsTask = getFormats(model);
            var languagesTask = getLanguages(model);

            await Task.WhenAll(categoriesTask, formatsTask, languagesTask);

            return new ObjectResult(model);
        }

        async Task getCategories(AvailableFiltersViewModel model)
        {
            model.Genres = await genreService.FindAllAsync();
        }

        async Task getFormats(AvailableFiltersViewModel model)
        {
            model.Formats = await formatService.FindAllAsync();
        }

        async Task getLanguages(AvailableFiltersViewModel model)
        {
            model.Languages = await languageService.FindAllAsync();
        }
    }
}
