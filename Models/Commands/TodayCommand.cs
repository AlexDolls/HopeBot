using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Net.Http;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using System.Net;

namespace HopeBot.Models.Commands
{
    public class TodayCommand : Command
    {
        public override string Name => "/today";
        string BaseUrl = "http://mygazeta.com/%D0%B3%D0%BE%D1%80%D0%BE%D1%81%D0%BA%D0%BE%D0%BF/%D0%B1%D0%BB%D0%B8%D0%B7%D0%BD%D0%B5%D1%86%D1%8B/%D0%B3%D0%BE%D1%80%D0%BE%D1%81%D0%BA%D0%BE%D0%BF-%D0%B1%D0%BB%D0%B8%D0%B7%D0%BD%D0%B5%D1%86%D1%8B-";

        public override async Task<bool> ExecuteAsync(Message message, TelegramBotClient client)
        {
            string url = BaseUrl + DateTime.Today.ToString("dd-MM-yyyy").Replace("-", "") + ".html";
            var response = await new HttpClient().GetAsync(url);
            string source = null;
            string all = "";
            MyGazetaParser parser = new MyGazetaParser();
            int i = 0;
            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }
            var domParser = new HtmlParser();
            var document = await domParser.ParseAsync(source);
            var result = parser.Parse(document);
            await client.SendTextMessageAsync(message.Chat.Id, url);
            foreach (var elem in result)
            {
                i++;
                if (i == 2 || i == 4 || i == 6 || i == 8 || i == 10 || i == 12 || i == 14 || i == 16 || i == 18 || i == 20 || i == 24)
                {
                    all += elem;

                }
            }
            await client.SendTextMessageAsync(message.Chat.Id, all);
            return true;

        }
    }
    class MyGazetaParser
    {
        public string[] Parse(IHtmlDocument document)
        {
            var list = new List<string>();
            var items = document.QuerySelectorAll("p");
            foreach (var item in items)
            {
                list.Add(item.TextContent);

            }
            return list.ToArray();
        }
    }
}