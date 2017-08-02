using System.Collections.Generic;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Net;
using System.Net.Http;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;

namespace HopeBot.Models.Commands.ParsMyGazeta
{
    public static class Parser
    {
        public static async void MyGazetaParseAsync(string url, Message message, TelegramBotClient client)
        {
            var list = new List<string>();
            string source = null;
            HttpClient Hclient = new HttpClient();
            var response = await Hclient.GetAsync(url);

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }
            IHtmlDocument doc = new HtmlParser().Parse(source);


            var items = doc.QuerySelectorAll("p").Where(item => item.ClassName == null);
            foreach (var item in items)
            {
                list.Add(item.TextContent);
            }
            for (int i = 1; i < 13; i++)
            {
                await client.SendTextMessageAsync(message.Chat.Id, list[i]);
            }
        }
    }
}