using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class Conversations
    {
        public Conversations()
        {
            Messages = new HashSet<Messages>();
        }

        public uint IdConversation { get; set; }
        public int IdUser { get; set; }
        public uint IdRecipient { get; set; }
        public uint Timestamp { get; set; }
        public string Subject { get; set; }

        public ICollection<Messages> Messages { get; set; }
    }
}
