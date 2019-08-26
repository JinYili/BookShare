using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookShare.Web.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Title { get; set; }

        [Display(Name = "Author")]
        public string Author { get; set; }

        [Display(Name = "Release date")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Image")]
        public string CoverUrl { get; set; }

        public List<Comment> Comments { get; set; }

    }
}

