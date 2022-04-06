namespace HelpDesk_AdminLte.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Clients
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Clients()
        {
            Requests = new HashSet<Requests>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Index(IsUnique = true)]
        public Guid ClientID { get; set; }
        

        [Required]
        [StringLength(50)]
        [Index("ClientNameIndex", IsUnique = true)]
        [Display(Name = "���")]
        public string Name { get; set; }

        [Display(Name = "�������")]
        public string Phone { get; set; }

        private bool _active = true;

        [Display(Name = "�������")]
        public bool Active { get { return _active; } set { _active = value; } }
        public string Email { get; set; }

        [Display(Name = "������������")]
        [StringLength(128)]
        public string UserID { get; set; }

        [Display(Name = "��������")]
        public int? CompanyID { get; set; }

        [Display(Name = "����")]
        public string Image { get; set; }

       public virtual Companies Companies { get; set; }
        public virtual  ApplicationUser User { get; set; }

        public virtual ICollection<Requests> Requests { get; set; }
    }
}

