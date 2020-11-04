using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Group
    {
        public int Id{ get; set; }
        [MaxLength(30)]
        [DisplayName("Grup Adı")]
        public string GroupName { get; set; }
        [DisplayName("Açıklaması")]
        public string Explain { get; set; }
       // public ICollection<Users> Users { get; set; }
    }
}
