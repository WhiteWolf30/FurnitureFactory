using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureFactory.Models
{
    public class SupplyItem
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Постачання")]
        public int SupplyId { get; set; }

        [Required]
        [Display(Name = "Матеріал")]
        public int MaterialId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,3)")]
        [Display(Name = "Кількість замовлена")]
        public decimal OrderedQuantity { get; set; }

        [Column(TypeName = "decimal(18,3)")]
        [Display(Name = "Кількість отримана")]
        public decimal ReceivedQuantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Ціна за одиницю (грн)")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Сума (грн)")]
        public decimal TotalPrice => ReceivedQuantity * UnitPrice;

        [StringLength(200)]
        [Display(Name = "Примітка до позиції")]
        public string Note { get; set; }

        // Навігаційні властивості
        public virtual Supply Supply { get; set; }
        public virtual Material Material { get; set; }
    }
}