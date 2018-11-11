using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class Conversations
    {
        public Conversations()
        {
            Msgs = new HashSet<Msgs>();
        }

        public uint IdConversation { get; set; }
        public uint IdUser { get; set; }
        public uint IdRecipient { get; set; }
        public uint Timestamp { get; set; }
        public string Subject { get; set; }

        public ICollection<Msgs> Msgs { get; set; }
    }
}
