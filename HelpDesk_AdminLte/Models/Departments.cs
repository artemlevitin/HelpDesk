namespace HelpDesk_AdminLte.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Departments
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Departments()
        {
            Support = new HashSet<Support>();
        }

        [Key]
        public int DepartmentID { get; set; }

        [StringLength(50)]
        [Display(Name = "Департамент")]
        [Index("DepartmentNameIndex", IsUnique = true)]
        public string Name { get; set; }

        private bool _active = true;
        [Display(Name = "Активен")]
        public bool Active { get { return _active; } set { _active = value; } }

        [Display(Name = "Расположение")]
        [StringLength(50)]
        public string Location { get; set; }

        [Display(Name = "Телефон")]
        [StringLength(50)]
        public string Phone { get; set; }

        [Display(Name = "Фото")]
        public string Image { get; set; }

        [Display(Name = "Детали")]
        public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Support> Support { get; set; }
    }
}
