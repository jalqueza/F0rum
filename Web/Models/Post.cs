using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        public Post()
        {
            DateTime = DateTime.Now;
        }


        [Required]
        public int ThreadId { get; set; }
        public Thread Thread { get; set; }


        public int? ReplyId { get; set; }
        public Post Reply { get; set; }


        public ICollection<Post> Replies { get; set;}
    }
}
