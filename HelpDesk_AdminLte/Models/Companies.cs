using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HelpDesk_AdminLte.Models
{
    public class Companies
    {
        public Companies()
        {
            Clients = new HashSet<Clients>();
        }

        [Key]
        public int CompanyID { get; set; }

        [StringLength(50)]
        [Display(Name = "Компания")]
        [Index("CompanyNameIndex", IsUnique = true)]

        public string Name { get; set; }

        private bool _active = true;
        [Display(Name = "Активна")]
        public bool Active { get { return _active; } set { _active = value; } }

        [Display(Name = "Адрес")]
        [StringLength(50)]
        public string Location { get; set; }

       
        public string Email { get; set; }
        [StringLength(50)]

        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        public virtual ICollection<Clients> Clients { get; set; }
    }
}