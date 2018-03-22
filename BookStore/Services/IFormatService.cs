using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.Services
{
    public interface IFormatService
    {
        Task<Format> CreateAsync(Format format);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteAsync(Format format);
        Task<ICollection<Format>> FindAllAsync();
        Task<Format> FindByIdAsync(int id);
        Task<Format> UpdateAsync(int Id, Format newFormat);
    }
}