using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Login.Controllers
{
    public class UserGroupForUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int BranchId { get; set; }
    }
}