using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.Services
{
    public interface IGenreService
    {
        Task<Genre> CreateAsync(Genre genre);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteAsync(Genre genre);
        Task<ICollection<Genre>> FindAllAsync();
        Task<Genre> FindByIdAsync(int id);
        Task<Genre> UpdateAsync(int Id, Genre newGenre);
    }
}