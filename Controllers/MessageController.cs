using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Telegram.Bot.Types;
using HopeBot.Models;

namespace HopeBot.Controllers
{
    public class MessageController : ApiController
    {
        [Route(@"api/message/update")] //webhook uri part
        public async Task<OkResult> Update([FromBody]Update update)
        {
            var commands = Bot.Commands;
            var message = update.Message;
            var client = await Bot.Get();

            foreach (var command in commands)
            {
                if (command.Contains(message.Text))
                {
                    if(!await command.ExecuteAsync(message, client))
                        await client.SendTextMessageAsync(message.Chat.Id, "упс, что-то пошло не так");
                    break;
                }
            }
            return Ok();
        }

    }
}