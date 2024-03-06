using MassTransit;
using Newtonsoft.Json;
using SharedModel;
using System.IdentityModel.Tokens.Jwt;

namespace AccountAPI.Common
{
    public class Consumer : IConsumer<MessageConsumer>
    {
        public async Task Consume(ConsumeContext<MessageConsumer> context)
        {
            var jsonMessage = JsonConvert.SerializeObject(context.Message);
            Console.WriteLine($"receive message: {jsonMessage}");
            var token = context.Message.token.Split(' ');
            var tokenHandler = new JwtSecurityTokenHandler();
            var result = tokenHandler.ReadToken(token[1]) as JwtSecurityToken;
            if (result != null)
            {
                string role = result.Claims.First(claim => claim.Type.Contains("role")).Value;
                if (role == Role.Student) await context.RespondAsync<MessageConsumer>(new { status = true, message = "You are student" });
                else await context.RespondAsync<MessageConsumer>(new { status = true, message = "You are admin" });
            }
            else await context.RespondAsync<MessageConsumer>(new { status = false, message = "Permission denied" });
        }
    }
}
