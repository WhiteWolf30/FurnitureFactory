using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FurnitureFactory.Models
{
    public class MaterialCategory
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва категорії є обов'язковою")]
        [StringLength(100)]
        [Display(Name = "Назва категорії")]
        public string Name { get; set; }

        [StringLength(300)]
        [Display(Name = "Опис")]
        public string Description { get; set; }

        // Навігаційна властивість
        public virtual ICollection<Material> Materials { get; set; }
    }
}