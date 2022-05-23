using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login.Models
{
    public class Permissions
    {
        public int Id { get; set; }
        public string ModuleName { get; set; }
        public string SubModuleName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
