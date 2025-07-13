using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDashBoard.Models
{
        public class EventViewModel
        {
       public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public IFormFile? Image { get; set; }  // صورة جديدة للرفع

        public string? ImageName { get; set; } // اسم الصورة المخزن سابقاً (للحفظ أو العرض)

        [Required]
        public int  Category { get; set; } 

        [Required]
        [Range(0, 1000000)]
        public decimal Price { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Venue { get; set; } = string.Empty;
    }
}



    


