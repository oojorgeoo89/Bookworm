using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Models.Helpers;
using BookStore.Models.ViewModels;
using BookStore.Repository;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public class BookService : IBookService
    {
        private readonly BookwormContext _context;

        public BookService(BookwormContext dbContext)
        {
            this._context = dbContext;
        }

        public async Task<ICollection<Book>> FindAllAsync(PageInfo pageInfo)
        {
            return await this.setOrderBy(_context.Books, pageInfo)
                           .Skip(pageInfo.Page * pageInfo.Size)
                           .Take(pageInfo.Size)
                           .ToListAsync();
        }

        public async Task<ICollection<Book>> SearchAsync(BookFilters bookFilters, PageInfo pageInfo)
        {
            IQueryable<Book> searchItems = _context.Books;

            searchItems = setFilters(searchItems, bookFilters);
            searchItems = setOrderBy(searchItems, pageInfo);

            return await searchItems
                            .Skip(pageInfo.Page * pageInfo.Size)
                            .Take(pageInfo.Size)
                            .Include(book => book.Genre)
                            .Include(book => book.Offers).ThenInclude(offer => offer.Format)
                            .Include(book => book.Offers).ThenInclude(offer => offer.Language)
                            .ToListAsync();
        }

        private static IQueryable<Book> setFilters(IQueryable<Book> searchItems, BookFilters bookFilters)
        {
            if (bookFilters.searchString != null)
            {
                searchItems = searchItems.Where(book => book.Title.Contains(bookFilters.searchString));
            }

            if (bookFilters.Genres.Count > 0)
            {
                searchItems = searchItems.Where(book => bookFilters.Genres.Contains(book.Genre.Id));
            }

            if (bookFilters.Formats.Count > 0)
            {
                searchItems = searchItems.Where(book => book.Offers.Any(offer => bookFilters.Formats.Contains(offer.Format.Id)));
            }

            if (bookFilters.Languages.Count > 0)
            {
                searchItems = searchItems.Where(book => book.Offers.Any(offer => bookFilters.Languages.Contains(offer.Language.Id)));
            }

            return searchItems;
        }

        private IQueryable<Book> setOrderBy(IQueryable<Book> books, PageInfo pageInfo)
        {
            switch (pageInfo.OrderBy)
            {
                case "date":
                case "date,asc":
                    return books.OrderBy(book => book.Id);
                case "date,desc":
                    return books.OrderByDescending(book => book.Id);
                case "alpha":
                case "alpha,asc":
                    return books.OrderBy(book => book.Title);
                case "alpha,desc":
                    return books.OrderByDescending(book => book.Title);
                case "price":
                case "price,asc":
                    return books.OrderBy(book => book.Offers.Min(offer => offer.Price));
                case "price,desc":
                    return books.OrderByDescending(book => book.Offers.Min(offer => offer.Price));
                default:
                    return books;
            }
        }

        public async Task<Book> FindByIdAsync(int id)
        {
            return await _context
                .Books
                .Include(book => book.Genre)
                .Include(book => book.Offers).ThenInclude(offer => offer.Format)
                .Include(book => book.Offers).ThenInclude(offer => offer.Language)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Book> CreateAsync(Book book)
        {
            if (book == null)
            {
                throw new ArgumentNullException();
            }

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return book;
        }

        public async Task<Book> UpdateAsync(int Id, Book newBook)
        {
            if (newBook == null)
            {
                return null;
            }

            var book = await _context.Books.FirstOrDefaultAsync(t => t.Id == Id);
            if (book == null)
            {
                return null;
            }

            book.Title = newBook.Title;
            book.Summary = newBook.Summary;
            book.Isbn = newBook.Isbn;
            book.Image = newBook.Image;

            _context.Books.Update(book);
            await _context.SaveChangesAsync();

            return book;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = _context.Books.FirstOrDefault(t => t.Id == id);
            if (book == null)
            {
                return false;
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Book book)
        {
            return await DeleteAsync(book.Id);
        }
    }
}
