using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using BotCallCenter.Domain;
using BotCallCenter.Utils;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace BotCallCenter
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                activity.ParseHtmlAttachments();
                var agentSip = activity.FindAgentSip();
                if (agentSip == "<NONE>")
                {
                    var target = Orchestrator.FindConversation(activity.From.Id);
                    if (target == null)
                    {
                        await Redirector.ReturnError(activity, "No active Conversation found");
                    }
                    else
                    {
                        await Redirector.RedirectToEndpoint(activity, target);
                    }
                }
                else
                {
                    var target = Orchestrator.FindAgent(agentSip);
                    if (target == null)
                    {
                        await Redirector.ReturnError(activity, "Agent not found");

                    }
                    else if (!Orchestrator.AddConversationFromActivity(target.Id, activity))
                    {
                        await Redirector.ReturnError(activity, "Agent currently busy");
                    }
                    else
                    {
                        await Redirector.RedirectToEndpoint(activity, target);
                    }
                }
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }
            else if (message.Type == ActivityTypes.EndOfConversation)
            {
                var agentSip = message.FindAgentSip();
                if (agentSip == "<NONE>") return null;

                var target = Orchestrator.FindAgent(agentSip);
                if (target != null)
                {
                    Orchestrator.RemoveActiveConversation(target.Id);
                }
            }

            return null;
        }
    }
}