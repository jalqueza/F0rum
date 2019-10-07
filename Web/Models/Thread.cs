using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Thread
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        public Thread()
        {
            DateTime = DateTime.Now;
        }


        public int BoardId { get; set; }
        public Board Board { get; set; }


        public ICollection<Post> Posts { get; set; }
    }
}
