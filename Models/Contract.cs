using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureFactory.Models
{
    public class Contract
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Номер договору є обов'язковим")]
        [StringLength(50)]
        [Display(Name = "Номер договору")]
        public string ContractNumber { get; set; }

        [Required]
        [Display(Name = "Постачальник")]
        public int SupplierId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Дата укладання")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата закінчення")]
        public DateTime? EndDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Сума договору (грн)")]
        public decimal TotalAmount { get; set; }

        [Display(Name = "Статус")]
        public ContractStatus Status { get; set; }

        [StringLength(500)]
        [Display(Name = "Примітки")]
        public string Notes { get; set; }

        // Навігаційні властивості
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<Supply> Supplies { get; set; }
    }

    public enum ContractStatus
    {
        [Display(Name = "Активний")] Active,
        [Display(Name = "Завершений")] Completed,
        [Display(Name = "Розірваний")] Terminated
    }
}