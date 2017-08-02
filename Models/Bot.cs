using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using HopeBot.Models.Commands;
using System.Data.SqlClient;

namespace HopeBot.Models
{
    public static class Bot
    {
        private static TelegramBotClient client;
        private static List<Command> commandsList;
        public static IReadOnlyList<Command> Commands => commandsList.AsReadOnly();
        public static SqlConnectionStringBuilder Str_Sql;
        public static async Task<TelegramBotClient> Get()
        {
            if(client != null)
            {
                return client;
            }

            commandsList = new List<Command>();
            commandsList.Add(new HelloCommand());
            commandsList.Add(new ServerInfoCommand());
            commandsList.Add(new RollCommand());
            commandsList.Add(new TodayCommand());

            Str_Sql = new SqlConnectionStringBuilder();
            Str_Sql.DataSource = Settings.DataSource;
            Str_Sql.UserID = Settings.UserId;
            Str_Sql.Password = Settings.Password;
            Str_Sql.InitialCatalog = Settings.InitialCatalog;

            client = new TelegramBotClient(Settings.Key);
            var hook = string.Format(Settings.Url, "api/message/update");
            await client.SetWebhookAsync(hook);
            return client;
        }
    }
}