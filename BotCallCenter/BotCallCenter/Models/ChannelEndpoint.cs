using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BotCallCenter.Models
{
    public class ChannelEndpoint
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ConversationId { get; set; }
        public string ServiceUrl { get; set; }

        public string ChannelId { get; set; }
        
    }
}