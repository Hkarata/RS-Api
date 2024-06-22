using MassTransit;
using Microsoft.Extensions.Logging;
using RSAllies.Test.Contracts.Requests;
using RSAllies.Test.Data;
using RSAllies.Test.Entities;
using RSAllies.Test.Services;

namespace RSAllies.Test.Messaging
{
    internal class ResponseConsumer(TestDbContext dbContext, ILogger<ResponseConsumer> logger) : IConsumer<UserResponseDto>
    {
        public Task Consume(ConsumeContext<UserResponseDto> context)
        {
            logger.LogInformation("ndcn");

            var score = MarkerService.Mark(dbContext, context.Message.Answers!);

            var userScore = new Score
            {
                Id = Guid.NewGuid(),
                UserId = context.Message.UserId,
                ScoreValue = score,
                CreatedAt = DateTime.UtcNow
            };

            dbContext.Scores.Add(userScore);

            dbContext.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
