using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace WebApplication1.Models
{
    public class Orders
    {
        public int Id { get; set; }
        [DisplayName("Başlık")]
        public string Title { get; set; }

        [DisplayName("Açıklama")]
        public string Explain { get; set; }

        [MaxLength(20)]
        [DisplayName("Durum")]
        public string Status { get; set; }


        //public int GrupId { get; set; }
        // public int UserId { get; set; }

        public int CreatorId { get; set; }

        
        [DisplayName("Oluşturma Tarihi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? CreateTime { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Başlangıç Tarihi")]
        public DateTime? StartTime { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Bitiş Tarihi")]
        public DateTime? FinishTime { get; set; }

        public virtual Group Group { get; set; }

        public virtual Users User { get; set; }
    }
}
