using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly BookwormContext _context;

        public LanguageService(BookwormContext dbContext)
        {
            this._context = dbContext;
        }

        public async Task<ICollection<Language>> FindAllAsync()
        {
            return await _context.Languages
                           .ToListAsync();
        }

        public async Task<Language> FindByIdAsync(int id)
        {
            return await _context.Languages.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Language> CreateAsync(Language language)
        {
            if (language == null)
            {
                throw new ArgumentNullException();
            }

            _context.Languages.Add(language);
            await _context.SaveChangesAsync();

            return language;
        }

        public async Task<Language> UpdateAsync(int Id, Language newLanguage)
        {
            if (newLanguage == null)
            {
                return null;
            }

            var language = await _context.Languages.FirstOrDefaultAsync(t => t.Id == Id);
            if (language == null)
            {
                return null;
            }

            language.Name = newLanguage.Name;

            _context.Languages.Update(language);
            await _context.SaveChangesAsync();

            return language;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var language = await _context.Languages.FirstOrDefaultAsync(t => t.Id == id);
            if (language == null)
            {
                return false;
            }

            _context.Languages.Remove(language);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Language language)
        {
            return await DeleteAsync(language.Id);
        }
    }
}
