using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace FurnitureFactory.Models
{
    public class Supplier
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Назва є обов'язковою")]
        [StringLength(200)]
        [Display(Name = "Назва компанії")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Контактна особа є обов'язковою")]
        [StringLength(100)]
        [Display(Name = "Контактна особа")]
        public string ContactPerson { get; set; }

        [Required(ErrorMessage = "Телефон є обов'язковим")]
        [Phone]
        [StringLength(20)]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [EmailAddress]
        [StringLength(150)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(300)]
        [Display(Name = "Адреса")]
        public string Address { get; set; }

        [StringLength(50)]
        [Display(Name = "ЄДРПОУ")]
        public string TaxCode { get; set; }

        [Display(Name = "Активний")]
        public bool IsActive { get; set; } = true;

        // Навігаційні властивості
        public virtual ICollection<Supply> Supplies { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
    }
}