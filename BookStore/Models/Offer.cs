using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    /*
     * 
     * A book may be available on different formats (paperback, eBook...)
     * and languages.
     * 
     * Offers link books with formats and languages while assigning them a price.
     * 
     */
    public class Offer
    {
        public int Id { get; set; }

        [Required]
        public Book Book { get; set; }

        [Required]
        public Format Format { get; set; }

        [Required]
        public Language Language { get; set; }

        [Required]
        public int Price { get; set; }
    }
}
