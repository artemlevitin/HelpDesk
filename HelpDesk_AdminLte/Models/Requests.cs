namespace HelpDesk_AdminLte.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public partial class Requests
    {
        [Key]
        [Display(Name = "№")]
        public int RequestID { get; set; }

        [Display(Name = "Инициатор")]
        public string CreatedByUserId { get; set; }

        [Display(Name = "Срок")]
        [DataType(DataType.Date)]
        public DateTime? DateRequest { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Заявка")]

        public string NameRequest { get; set; }

        [Display(Name = "Подробности")]
        [AllowHtml]
        public string Description { get; set; }

        [Display(Name = "Клиент")]
        public Guid? ClientID { get; set; }

        [Display(Name = "Ответсвенный")]
        public Guid? SupportID { get; set; }

        [Display(Name = "Статус")]
        public int? StatusID { get; set; }

        [Column(TypeName = "datetime2")]
        [Display(Name = "Создан")]
        public DateTime? DateCreateRequest { get; set; }

        [Display(Name = "Решение")]
        [AllowHtml]
        public string CommentResolve { get; set; }

        [Display(Name = "Приоритет")]
        public int? PriorityID { get; set; }

        [Display(Name = "Файл")]
        public string File { get; set; }

        public string FilePath { get; set; }

      

        public virtual Clients Clients{ get; set; }

        public virtual Priority Priority { get; set; }

        public virtual Status Status { get; set; }

        public virtual Support Support { get; set; }
        public virtual ApplicationUser User { get; set; }

        public static explicit operator Requests(Task<Requests> v)
        {
            throw new NotImplementedException();
        }
    }
}
