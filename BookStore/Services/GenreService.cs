using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public class GenreService : IGenreService
    {
        private readonly BookwormContext _context;

        public GenreService(BookwormContext dbContext)
        {
            this._context = dbContext;
        }

        public async Task<ICollection<Genre>> FindAllAsync()
        {
            return await _context.Genres
                           .ToListAsync();
        }

        public async Task<Genre> FindByIdAsync(int id)
        {
            return await _context.Genres.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Genre> CreateAsync(Genre genre)
        {
            if (genre == null)
            {
                throw new ArgumentNullException();
            }

            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();

            return genre;
        }

        public async Task<Genre> UpdateAsync(int Id, Genre newGenre)
        {
            if (newGenre == null)
            {
                return null;
            }

            var genre = await _context.Genres.FirstOrDefaultAsync(t => t.Id == Id);
            if (genre == null)
            {
                return null;
            }

            genre.Name = newGenre.Name;

            _context.Genres.Update(genre);
            await _context.SaveChangesAsync();

            return genre;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(t => t.Id == id);
            if (genre == null)
            {
                return false;
            }

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Genre genre)
        {
            return await DeleteAsync(genre.Id);
        }

    }
}
