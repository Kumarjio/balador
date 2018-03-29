using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Michal.Balador.Server.Models
{
    [TableAttribute("Balador_ClientMessage",Schema ="dbo")]
    public class ClientMessage
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string ClientId { get; set; }
        [Required]
        public string AccountId { get; set; }
        [Required]
        public string Messsage { get; set; }
        [Required]
        public string MesssageType { get; set; }
        
        [Required]
        public DateTime CreatedOn { get; set; }
        [Required]
        public DateTime ModifiedOn { get; set; }

        public Guid? ConversationId { get; set; }
        public Guid? JobId { get; set; }


    }
}