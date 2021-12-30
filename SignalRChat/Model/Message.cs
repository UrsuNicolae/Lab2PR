using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Model
{
    public class Message
    {
        [Required]
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string User { get; set; }

        [MaxLength(1000)]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
