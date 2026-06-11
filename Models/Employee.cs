using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FurnitureFactory.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Прізвище є обов'язковим")]
        [StringLength(100)]
        [Display(Name = "Прізвище")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Ім'я є обов'язковим")]
        [StringLength(100)]
        [Display(Name = "Ім'я")]
        public string FirstName { get; set; }

        [StringLength(100)]
        [Display(Name = "По батькові")]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Посада")]
        public string Position { get; set; }

        [Phone]
        [StringLength(20)]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Display(Name = "Активний")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "ПІБ")]
        public string FullName => $"{LastName} {FirstName} {MiddleName}".Trim();

        // Навігаційна властивість
        public virtual ICollection<Supply> Supplies { get; set; }
    }
}