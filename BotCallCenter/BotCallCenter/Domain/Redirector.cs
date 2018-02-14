using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using BotCallCenter.Data;
using BotCallCenter.Models;
using Microsoft.Bot.Connector;
using Newtonsoft.Json.Linq;

namespace BotCallCenter.Domain
{
    public static class Redirector
    {

        

        public static async Task RedirectToEndpoint(IMessageActivity oldActivity, ChannelEndpoint endpoint)
        {
            var connectorClient = new ConnectorClient(new Uri(endpoint.ServiceUrl));
            MicrosoftAppCredentials.TrustServiceUrl(endpoint.ServiceUrl);
            Agents.BotAccounts.TryGetValue(endpoint.ChannelId, out var botAccount);

            var userAccount = new ChannelAccount(endpoint.Id, endpoint.Name);

            try
            {
                //var conversation = connectorClient.Conversations.CreateOrGetDirectConversation(botAccount, userAccount, Agents.TenantId);

                var activity = Activity.CreateMessageActivity();

                activity.Text = $"{oldActivity.From.Name}: {oldActivity.Text}";
                activity.ChannelId = endpoint.ChannelId;
                activity.From = botAccount;
                activity.Recipient = userAccount;

                activity.Conversation = new ConversationAccount(id: endpoint.ConversationId);
                await connectorClient.Conversations.SendToConversationAsync((Activity) activity);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }

        public static async Task ReturnError(Activity activity, string message)
        {
            var connectorClient = new ConnectorClient(new Uri(activity.ServiceUrl));

            var newActivity = activity.CreateReply(message);
            await connectorClient.Conversations.ReplyToActivityAsync(newActivity);

        }


    }
}