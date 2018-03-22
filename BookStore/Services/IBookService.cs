using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Models.Helpers;
using BookStore.Models.ViewModels;

namespace BookStore.Services
{
    public interface IBookService
    {
        Task<Book> CreateAsync(Book book);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteAsync(Book book);
        Task<ICollection<Book>> FindAllAsync(PageInfo pageInfo);
        Task<Book> FindByIdAsync(int id);
        Task<ICollection<Book>> SearchAsync(BookFilters bookFilters, PageInfo pageInfo);
        Task<Book> UpdateAsync(int Id, Book newBook);
    }
}