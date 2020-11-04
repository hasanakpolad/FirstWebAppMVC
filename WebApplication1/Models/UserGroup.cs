using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class UserGroup
    {
        public int Id { get; set; }
        //public int UserId { get; set; }
        //public int GroupId { get; set; }
        public Users User { get; set; }
        public Group Group { get; set; }
    }
}
