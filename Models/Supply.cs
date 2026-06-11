using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurnitureFactory.Models
{
    public class Supply
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Номер накладної є обов'язковим")]
        [StringLength(50)]
        [Display(Name = "Номер накладної")]
        public string InvoiceNumber { get; set; }

        [Required]
        [Display(Name = "Постачальник")]
        public int SupplierId { get; set; }

        [Display(Name = "Договір")]
        public int? ContractId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Дата постачання")]
        public DateTime SupplyDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Очікувана дата")]
        public DateTime? ExpectedDate { get; set; }

        [Display(Name = "Статус")]
        public SupplyStatus Status { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Загальна сума (грн)")]
        public decimal TotalAmount { get; set; }

        [Required]
        [Display(Name = "Відповідальний")]
        public int ResponsibleEmployeeId { get; set; }

        [StringLength(500)]
        [Display(Name = "Примітки")]
        public string Notes { get; set; }

        // Навігаційні властивості
        public virtual Supplier Supplier { get; set; }
        public virtual Contract Contract { get; set; }
        public virtual Employee ResponsibleEmployee { get; set; }
        public virtual ICollection<SupplyItem> SupplyItems { get; set; }
    }

    public enum SupplyStatus
    {
        [Display(Name = "Очікується")] Pending,
        [Display(Name = "В дорозі")] InTransit,
        [Display(Name = "Отримано")] Received,
        [Display(Name = "Скасовано")] Cancelled
    }
}