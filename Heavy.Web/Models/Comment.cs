using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookShare.Web.Models
{
    public class Comment
    {

        public int Id { get; set; }

        public string Content { get; set; }
        public DateTime CreateDate { get; set; }

        public int bookId { get; set; }
        public Book book { get; set; }

    }
}
