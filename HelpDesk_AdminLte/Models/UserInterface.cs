namespace HelpDesk_AdminLte.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserInterface")]
    public partial class UserInterface
    {
        [Key]
        public string AspNetUserID { get; set; }

        public bool? LayoutBoxed { get; set; }

        public bool? SkinColor { get; set; }

        public bool? LayoutFixed { get; set; }

        public bool? SidebarCollapsed { get; set; }

        //public virtual AspNetUsers AspNetUsers { get; set; }
    }
}
