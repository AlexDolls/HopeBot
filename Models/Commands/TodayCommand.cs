using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;
using HopeBot.Models.Commands.ParsMyGazeta;

namespace HopeBot.Models.Commands
{
    public class TodayCommand : Command
    {
        public override string Name => "/Today";

        public override async Task<bool> ExecuteAsync(Message message, TelegramBotClient client)
        {
            string url = "http://mygazeta.com/%D0%B3%D0%BE%D1%80%D0%BE%D1%81%D0%BA%D0%BE%D0%BF/%D0%B1%D0%BB%D0%B8%D0%B7%D0%BD%D0%B5%D1%86%D1%8B/%D0%B3%D0%BE%D1%80%D0%BE%D1%81%D0%BA%D0%BE%D0%BF-%D0%B1%D0%BB%D0%B8%D0%B7%D0%BD%D0%B5%D1%86%D1%8B-" + DateTime.Today.ToString().Remove(10).Replace(".", "") + ".html";
            Parser.MyGazetaParseAsync(url, message, client);
            return true;
        }
    }
}