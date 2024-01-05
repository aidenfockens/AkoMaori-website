using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace A2.Dtos
{
    public class EventDto
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Start { get; set; }
        [Required]
        public string End { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        [Required]
        public string Location { get; set; }
    }
}
