using OnlyCatsWebapp.Areas.Identity.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlyCatsWebapp.Models
{
    public class Cat
    {
        [Key]
        public int CatId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date added")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateAdded { get; set; }

        //public List<string>? Images { get; set; }

        public string? ImageName { get; set; }

        public string? ImageLocation { get; set; }

        public Age? Age { get; set; }

        [Required]
        public string UserId { get; set; }
    }

    public enum Age
    {
        Kitten,
        Adult,
        Senior
    }


}
