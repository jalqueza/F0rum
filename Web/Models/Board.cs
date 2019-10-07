using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Board
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }


        public ICollection<Thread> Threads { get; set;}
    }
}
