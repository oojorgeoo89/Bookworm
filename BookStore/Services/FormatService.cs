using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public class FormatService : IFormatService
    {
        private readonly BookwormContext _context;

        public FormatService(BookwormContext dbContext)
        {
            this._context = dbContext;
        }

        public async Task<ICollection<Format>> FindAllAsync()
        {
            return await _context.Formats
                           .ToListAsync();
        }

        public async Task<Format> FindByIdAsync(int id)
        {
            return await _context.Formats.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Format> CreateAsync(Format format)
        {
            if (format == null)
            {
                throw new ArgumentNullException();
            }

            _context.Formats.Add(format);
            await _context.SaveChangesAsync();

            return format;
        }

        public async Task<Format> UpdateAsync(int Id, Format newFormat)
        {
            if (newFormat == null)
            {
                return null;
            }

            var format = await _context.Formats.FirstOrDefaultAsync(t => t.Id == Id);
            if (format == null)
            {
                return null;
            }

            format.Name = newFormat.Name;

            _context.Formats.Update(format);
            await _context.SaveChangesAsync();

            return format;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var format = await _context.Formats.FirstOrDefaultAsync(t => t.Id == id);
            if (format == null)
            {
                return false;
            }

            _context.Formats.Remove(format);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Format format)
        {
            return await DeleteAsync(format.Id);
        }
    }
}
