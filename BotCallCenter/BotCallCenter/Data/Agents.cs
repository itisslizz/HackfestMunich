using BotCallCenter.Models;

namespace BotCallCenter.Data
{
    public static class Agents
    {


        //public static readonly ChannelEndpoint AgentAdrien = new ChannelEndpoint
        //{
        //    Id =
        //        "29:1YwqHKllKePlvnxfY8GjgMQPi7lgw6Op7pOcwER1nc04tZuyJS347mFX0GCVgZvgC0CI0dTS7WH_FJhlEaIQPGw",
        //    Name = "aosolis(Guest)",
        //    ConversationId =
        //        "a:15MSHD2ccH5oGFF_-pmdTmgi1OtZwzDr5bKwxBVVkf6gzJfgSj2t0lAiw8WU0nlNB5o5VYIh8pH27bN0mMDtLX08DlK--oZq_tvGAqcf5w1KOIUMYitrQi3MqYkFvX5Mb",
        //    ServiceUrl = "https://smba.trafficmanager.net/emea-client-ss.msg/"
        //};

        public static readonly string AgentMarcSip = "sip:mhueppin@luware.com";

        public static readonly ChannelEndpoint AgentMarc = new ChannelEndpoint
        {
            Id =
                "29:1fOtrmeK2rOSHD7VUVCrdsOAFG_x1wlMtuf44V_7QiNB7nMEj7cJ8jJZ96SE9o6GzTPmFPfvVi6MeOCkl-O2sqg",
            Name = "Marc Hueppin",
            ConversationId =
                "a:1QBS-xMzdo4DJ2LEO1mZEgHLjdFHOBWwwEJOTCFzcAX0nKtODPpOMUrcsjoiIe9-o_ZHxP2ZGkZUygn2_7nJlV-FKypaPInSleGX96LaRoiYzZpM2AVanRf0Dw2wJYLAZ",
            ServiceUrl = "https://smba.trafficmanager.net/emea-client-ss.msg/"
        };
    }
}