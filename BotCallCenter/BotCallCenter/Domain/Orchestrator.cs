using System.Collections.Concurrent;
using System.Collections.Generic;
using BotCallCenter.Data;
using BotCallCenter.Models;
using Microsoft.Bot.Builder.ConnectorEx;
using Microsoft.Bot.Connector;

namespace BotCallCenter.Domain
{
    public static class Orchestrator
    {
        private static object _lock;
        private static readonly Dictionary<string, ChannelEndpoint> Agents = new Dictionary<string, ChannelEndpoint>
        {
            {Data.Agents.AgentMarcSip, Data.Agents.AgentMarc}
        };

        private static readonly ConcurrentDictionary<string, ChannelEndpoint> ActiveConversations =
            new ConcurrentDictionary<string, ChannelEndpoint>();

        public static ChannelEndpoint FindAgent(string sipUri)
        {
            return Agents.TryGetValue(sipUri, out var channelEndpoint) ? channelEndpoint : null;
        }

        public static ChannelEndpoint FindConversation(string agentId)
        {
            return ActiveConversations.TryGetValue(agentId, out var channelEndpoint) ? channelEndpoint : null;
        }

        public static bool AddConversationFromActivity(string agentId, Activity activity)
        {
            return ActiveConversations.TryAdd(agentId, new ChannelEndpoint()
            {
                Id = activity.From.Id,
                Name = activity.From.Name,
                ConversationId = activity.Conversation.Id,
                ServiceUrl = activity.ServiceUrl
            });
        }

        public static void RemoveActiveConversation(string agentId)
        {
            ActiveConversations.TryRemove(agentId, out var _);
        }
    }
}