using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Notes
    {
        public int Id { get; set; }

        [DisplayName("Başlık")]
        [Required]
        public string Title { get; set; }

        [DisplayName("Not")]
        [Required]
        public string Note { get; set; }

        [DisplayName("Genel")]
        public bool General { get; set; }

        // public int UserId { get; set; }
        // public int CategoryId { get; set; }

        public virtual Users User { get; set; }

        public virtual Category Category { get; set; }
    }
}
