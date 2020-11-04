using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Category
    {
        public int Id { get; set; }

        [MaxLength(20)]
        [DisplayName("Kategori Adı")]
        public string CategoryName{ get; set; }

        public virtual ICollection<Notes> Notes { get; set; }
        public Category()
        {
            Notes = new HashSet<Notes>();
        }
    }
}
