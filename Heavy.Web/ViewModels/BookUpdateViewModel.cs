using System;
using System.ComponentModel.DataAnnotations;


namespace BookShare.Web.ViewModels
{
    public class BookUpdateViewModel
    {
        [Display(Name = "Book name")]
        [Required(ErrorMessage = "{0} is Required"), MaxLength(100, ErrorMessage = "{0} length can't more than {1}")]
        public string Title { get; set; }

        [Display(Name = "Author")]
        [Required(ErrorMessage = "{0} is Required"), MaxLength(100, ErrorMessage = "{0} length can't more than {1}")]
        public string Artist { get; set; }

        [Display(Name = "Release date")]
        [DataType(DataType.DateTime)]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "{0} is Required"), Range(1, 200, ErrorMessage = "{0} Price must between {1} and {2}")]
        public decimal Price { get; set; }

        [Display(Name = "Image")]
        [Required(ErrorMessage = "{0} is Required"), MaxLength(200, ErrorMessage = "{0} length can't more than {1}")]
        public string CoverUrl { get; set; }
    }
}
