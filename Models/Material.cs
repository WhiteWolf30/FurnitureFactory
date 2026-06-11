using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureFactory.Models
{
    public class Material
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва матеріалу є обов'язковою")]
        [StringLength(200)]
        [Display(Name = "Назва матеріалу")]
        public string Name { get; set; }

        [StringLength(50)]
        [Display(Name = "Артикул")]
        public string ArticleNumber { get; set; }

        [Required(ErrorMessage = "Одиниця виміру є обов'язковою")]
        [StringLength(20)]
        [Display(Name = "Одиниця виміру")]
        public string UnitOfMeasure { get; set; } // шт, кг, м², м³, л

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Мінімальний запас")]
        public decimal MinStockLevel { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Поточний запас")]
        public decimal CurrentStock { get; set; }

        [StringLength(500)]
        [Display(Name = "Опис")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Категорія")]
        public int MaterialCategoryId { get; set; }

        // Навігаційні властивості
        public virtual MaterialCategory Category { get; set; }
        public virtual ICollection<SupplyItem> SupplyItems { get; set; }
    }
}