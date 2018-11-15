using System;
using System.Collections.Generic;

namespace FactuCR.Models
{
    public partial class Mesages
    {
        public uint IdMessage { get; set; }
        public uint Timestamp { get; set; }
        public string Ip { get; set; }
        public uint IdSender { get; set; }
        public uint IdRecipient { get; set; }
        public string Text { get; set; }
        public int Channel { get; set; }
        public uint Attachments { get; set; }
        public uint IdConversation { get; set; }

        public Conversations IdConversationNavigation { get; set; }
    }
}
