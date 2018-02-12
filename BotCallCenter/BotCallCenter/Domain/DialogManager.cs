using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BotCallCenter.Configuration;
using BotCallCenter.Models;
using Microsoft.Bot.Connector;

namespace BotCallCenter.Domain
{
    public static class DialogManager
    {
        private static readonly Dictionary<string, ChannelEndpoint> _agents = new Dictionary<string, ChannelEndpoint>
        {
            {Agents.AgentMarc.Id, Agents.AgentMarc}
        };

        private static readonly Dictionary<string, ChannelEndpoint> _activeConversations = new Dictionary<string, ChannelEndpoint>();




        public static ChannelEndpoint GetChannelEndpoint(Activity activity)
        {
            if (_agents.ContainsKey(activity.From.Id))
            {
                // is message from agent
                var found = _activeConversations.TryGetValue(activity.From.Id, out var client);
                return found ? client : null;
            }

            // is message from client
            var existingAgent =_activeConversations.Where(y => y.Value.Id == activity.From.Id).Select(x => x.Key);
            if (existingAgent.Any())
            {
                _agents.TryGetValue(existingAgent.FirstOrDefault(), out var existing);
                return existing;
            }
            var agent = _agents.Values.FirstOrDefault(x => !_activeConversations.Keys.Contains(x.Id));
            if (agent == null)
            {
                return null;
            }

            _activeConversations.Add(agent.Id, new ChannelEndpoint
            {
                Id = activity.From.Id,
                Name = activity.From.Name,
                ConversationId = activity.Conversation.Id,
                ServiceUrl = activity.ServiceUrl
            });
            return agent;
        }
    }
}