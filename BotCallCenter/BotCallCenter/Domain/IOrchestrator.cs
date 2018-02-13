using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotCallCenter.Models;

namespace BotCallCenter.Domain
{
    public interface IOrchestrator
    {
        ChannelEndpoint FindAgent(string sipUri);

        ChannelEndpoint FindConversation(string agentId);
    }
}
