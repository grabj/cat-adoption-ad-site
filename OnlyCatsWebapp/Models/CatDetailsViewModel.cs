using OnlyCatsWebapp.Areas.Identity.Data;

namespace OnlyCatsWebapp.Models
{
    public class CatDetailsViewModel
    {
        public Cat Cat { get; set; }
        public IFormFile? Photo { get; set; }
        public ApplicationUser User { get; set; }
    }
}
