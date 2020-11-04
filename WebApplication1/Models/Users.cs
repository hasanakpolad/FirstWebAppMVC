using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class Users
    {
        public int Id { get; set; }

        [DisplayName("Kullanıcı Adı")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string Name { get; set; }

        [DisplayName("Kullanıcı Soyadı")]
        [Required(ErrorMessage = "{0} alanı boş geçilmez.")]
        public string Surname { get; set; }

        [DisplayName("Telefon Numarası")]
        [MaxLength(11)]
        public string Phone { get; set; }

        [DisplayName("E-posta adresi")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Hatalı e-posta girişi.")]
        public string Mail { get; set; }
        [DisplayName("Yetki")]
        public Rank Rank { get; set; }

        public virtual ICollection<UserGroup> Groups { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
       
       public virtual ICollection<Notes> Notes { get; set; }
        
        public virtual ICollection<Daily> Daily { get; set; }

        public Users()
        {
            Groups = new HashSet<UserGroup>();
            Orders = new HashSet<Orders>();
            Notes = new HashSet<Notes>();
            Daily = new HashSet<Daily>();

        }
    }

    public enum Rank
    {
        [Display(Name ="Kullanıcı")]
        Kullanıcı,

        [Display(Name = "Yetkili")]
        Yetkili,
        [Display(Name = "Admin")]
        Admin
    }
}


