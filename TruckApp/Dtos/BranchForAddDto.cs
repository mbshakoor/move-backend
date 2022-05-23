using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login.Dtos
{
    public class BranchForAddDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
