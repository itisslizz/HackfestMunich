using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using BotCallCenter.Models;
using Microsoft.Bot.Connector;

namespace BotCallCenter.Domain
{
    public static class Redirector
    {

        

        public static async Task RedirectToEndpoint(IMessageActivity activity, ChannelEndpoint endpoint)
        {
            var connectorClient = new ConnectorClient(new Uri(endpoint.ServiceUrl));

            activity.From = activity.Recipient;

            activity.Recipient = new ChannelAccount(endpoint.Id, endpoint.Name);

            activity.Conversation = new ConversationAccount(id: endpoint.ConversationId);

            await connectorClient.Conversations.SendToConversationAsync((Activity) activity);
        }


    }
}