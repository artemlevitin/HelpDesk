using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelpDesk_AdminLte.Models
{
    public class Conversation
    {
        [Key]
        public int ConversationID { get; set; }

        [Required]
        [StringLength(128)]
        public string OneUserID { get; set; }
    
        [Required]
       [StringLength(128)]
        public string TwoUserID { get; set; }
       
        private bool _active = true;

        public Conversation( )
        {
        }

        [Display(Name = "Активен")]
        public bool Active { get { return _active; } set { _active = value; } }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Messages> Messages { get; set; }

        

    }
}