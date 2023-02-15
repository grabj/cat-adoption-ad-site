using System.Diagnostics.Metrics;

namespace OnlyCatsWebapp.Models
{
    public class CatViewModel
    {
        public Cat Cat { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
