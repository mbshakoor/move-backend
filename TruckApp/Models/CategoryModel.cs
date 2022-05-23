using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TruckApp.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Icon { get; set; }
        public byte [] IconByte { get; set; }
        public byte [] ImageByte { get; set; }
    }
}
