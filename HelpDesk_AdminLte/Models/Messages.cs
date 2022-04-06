using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HelpDesk_AdminLte.Models
{
    [Table("Messages")]
    public class Messages
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Messages()
        {
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Index(IsUnique = true)]
        public int MessageID { get; set; }


        [Display(Name = "Отправлен")]
        [DataType(DataType.Date)]
        public DateTime? DateCreate { get; set; }

        [Display(Name = "Прочтено")]
        [DataType(DataType.Date)]
        public DateTime? DateRead { get; set; }

        
        [Required]
        [StringLength(200)]
        [Display(Name = "Сообщение")]
        public string Text { get; set; }

        [Required]
        [Display(Name = "Отправитель")]
        [StringLength(128)]
        public string FromUserID { get; set; }

        
        [Display(Name = "Получатель")]
        [StringLength(128)]
        public string ToUserID { get; set; }



        [Display(Name = "Беседа")]
        public int? ConversationID { get; set; }

       

       
        public virtual Conversation Conversation { get; set; }

    }
}