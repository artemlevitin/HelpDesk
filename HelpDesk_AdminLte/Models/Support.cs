namespace HelpDesk_AdminLte.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Support")]
    public partial class Support
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Support()
        {
            Requests = new HashSet<Requests>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Index(IsUnique = true)]
        public Guid SupportID { get; set; }

        private bool _active = true;

        [Required]
        [StringLength(50)]
        [Display(Name = "Сотрудник")]
        [Index("SupportNameIndex", IsUnique = true)]

        public string Name { get; set; }

        [StringLength(50)]
        [Display(Name = "Специальность")]
        public string Speciality { get; set; }

        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        public string Email { get; set; }


        [Display(Name = "Активен")]
        public bool Active { get {return _active; } set { _active = value; } }

        [Display(Name = "Отдел")]

        public int? DepartamentID { get; set; }

        [Display(Name = "Пользователь")]

        [StringLength(128)]
        //[Index(IsUnique = true)]
        public string UserID { get; set; }

        [Display(Name = "Фото")]
        public string Image { get; set; }

        public virtual ApplicationUser User { get; set; }
       

        public virtual Departments Departments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Requests> Requests { get; set; }
    }
}
