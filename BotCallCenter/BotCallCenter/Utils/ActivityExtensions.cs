using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Connector;

namespace BotCallCenter.Utils
{
    public static class ActivityExtensions
    {
        public static Activity RemoveAttachements(this Activity activity)
        {
            activity.Attachments.Clear();
            return activity;
        }

        public static Activity ParseHtmlAttachments(this Activity activity)
        {
            foreach (var attachement in activity.Attachments)
            {
                if (attachement.ContentType != "text/html") continue;
                activity.Text = attachement.Content.ToString();
                activity.TextFormat = "xml";
            }

            activity.Attachments.Clear();
            return activity;
        }

        public static string FindAgentSip(this Activity activity)
        {
            var agentSip = "<NONE>";
            foreach (var entity in activity.Entities)
            {
                if (entity.Type == "Agent")
                {
                    foreach (var property in entity.Properties)
                    {
                        if (property.Key == "AgentSip")
                        {
                            agentSip = property.Value.ToString();
                        }
                    }
                }
            }

            return agentSip;
        }
    }
}