using System;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Models
{
    public class BookwormUser : IdentityUser
    {
        public string Address { get; set; }
        public Boolean IsAdmin { get; set; }
    }
}
