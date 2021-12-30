using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Messaging
{
    public class MessageOptions
    {
        public MessageOptions()
        {
            fromDisplayName = "SignalRChat";
            fromEmailAddress = "SignalRChat@gmail.com";
        }
        public string fromDisplayName { get; private set; }

        public string fromEmailAddress { get; private set; }

        public string toEamilAddress { get; set; }

        public string subjcet { get; set; }

        public string message { get; set; }
    }
}
