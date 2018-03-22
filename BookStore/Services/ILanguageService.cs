using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.Services
{
    public interface ILanguageService
    {
        Task<Language> CreateAsync(Language language);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteAsync(Language language);
        Task<ICollection<Language>> FindAllAsync();
        Task<Language> FindByIdAsync(int id);
        Task<Language> UpdateAsync(int Id, Language newLanguage);
    }
}