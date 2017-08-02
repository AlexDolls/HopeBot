using Telegram.Bot;
using Telegram.Bot.Types;
using System.Threading.Tasks;

namespace HopeBot.Models.Commands
{
    public abstract class Command
    {
        public abstract string Name { get; }

        public abstract Task<bool> ExecuteAsync(Message message, TelegramBotClient client);

        public bool Contains(string command)
        {
            return (command==this.Name);
        }

    }
}