using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Title { get; set; }
        public string Summary { get; set; }

        [Required]
        [MaxLength(14)]
        public string Isbn { get; set; }

        public string Image { get; set; }

        public Genre Genre { get; set; }

        public ICollection<Offer> Offers { get; set; }
    }
}
