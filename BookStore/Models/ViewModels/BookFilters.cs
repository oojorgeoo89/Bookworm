using System;
using System.Collections.Generic;

namespace BookStore.Models.ViewModels
{
    public class BookFilters
    {
        public BookFilters()
        {
            Genres = new List<int>();
            Languages = new List<int>();
            Formats = new List<int>();
        }

        public ICollection<int> Genres { get; set; }
        public ICollection<int> Languages { get; set; }
        public ICollection<int> Formats { get; set; }
        public string searchString { get; set; }
    }
}
