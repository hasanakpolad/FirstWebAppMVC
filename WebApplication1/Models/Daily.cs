using Bogus.DataSets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Daily
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Başlangıç Zamanı")]
        public DateTime StartTime { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Bitiş Zamanı")]
        public DateTime FinishTime { get; set; }

        [DisplayName("Notlar")]
        public string Note { get; set; }

        [MaxLength(20)]
        [DisplayName("Durum")]
        public string Status { get; set; }

        // public int UserId { get; set; }
        public Users User { get; set; }
    }
}
