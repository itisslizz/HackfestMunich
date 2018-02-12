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
    }
}