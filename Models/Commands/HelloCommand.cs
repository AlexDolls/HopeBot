using Telegram.Bot;
using Telegram.Bot.Types;
using System.Threading.Tasks;


namespace HopeBot.Models.Commands
{
    public class HelloCommand : Command
    {
        public override string Name => "/hello";

        public override async Task<bool> ExecuteAsync(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            await client.SendTextMessageAsync(chatId, "hello, dude!");
            return true;
        }
    }
}