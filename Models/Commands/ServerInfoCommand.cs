using Telegram.Bot;
using Telegram.Bot.Types;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace HopeBot.Models.Commands
{
    public class ServerInfoCommand : Command
    {
        public override string Name => "/server";

        public override async Task<bool> ExecuteAsync(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            try
            {
                using (SqlConnection connection = new SqlConnection(Bot.Str_Sql.ConnectionString))
                {
                    connection.Open();
                    string output = connection.ConnectionString + "\n" +
                        "==========================\n" +
                        "Server:[" + connection.DataSource + "]" + "\n" +
                        "Version:" + connection.ServerVersion + "\n" +
                        "State:" + connection.State;
                    await client.SendTextMessageAsync(chatId, output);
                    connection.Close();
                    return true;
                }
            }
            catch (SqlException e)
            {
                await client.SendTextMessageAsync(chatId, e.Number.ToString());
                return false;
            }
        }
    }
}