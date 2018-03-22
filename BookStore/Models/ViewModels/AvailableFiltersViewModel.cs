using System;
using System.Collections.Generic;

namespace BookStore.Models.ViewModels
{
    public class AvailableFiltersViewModel
    {
        public AvailableFiltersViewModel() {
            Genres = new List<Genre>();
            Languages = new List<Language>();
            Formats = new List<Format>();
        }

        public ICollection<Genre> Genres { get; set; }
        public ICollection<Language> Languages { get; set; }
        public ICollection<Format> Formats { get; set; }
    }
}
