using System;
using System.IO;
using System.Net;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace HopeBot.Models.Commands
{
    public class RollCommand : Command
    {
        public override string Name => "/roll";

        public override async Task<bool> ExecuteAsync(Message message, TelegramBotClient client)
        {
            try
            {
                await client.SendChatActionAsync(message.Chat.Id, ChatAction.UploadPhoto);
                await client.GetChatAsync(message.Chat.Id);

                SqlCommand command = new SqlCommand();

                Random rnd = new Random();
                string NumPic = rnd.Next(1, 310).ToString();

                using (SqlConnection connection = new SqlConnection(Bot.Str_Sql.ConnectionString))
                {
                    await connection.OpenAsync();
                    command.CommandText = ("SELECT * FROM Grils WHERE Id=" + NumPic);
                    command.Connection = connection;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        object UrlPic = reader["PicUrl"];
                        WebRequest req = HttpWebRequest.Create(UrlPic.ToString());
                        //client.SendTextMessage(message.Chat.Id, UrlPic.ToString());
                        using (Stream stream = req.GetResponse().GetResponseStream())
                        {
                            var fts = new FileToSend("1", stream);
                            await client.SendPhotoAsync(message.Chat.Id, fts);
                        }
                    }
                    connection.Close();
                    return true;
                }
            }
            catch (Exception e)
            {
                await client.SendTextMessageAsync(message.Chat.Id, "error");
                return false;
            }
        }
    }
}