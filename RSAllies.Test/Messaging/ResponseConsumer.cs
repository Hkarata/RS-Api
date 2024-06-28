using MassTransit;
using Microsoft.Extensions.Logging;
using RSAllies.Test.Contracts.Requests;
using RSAllies.Test.Data;
using RSAllies.Test.Entities;
using RSAllies.Test.Services;

namespace RSAllies.Test.Messaging
{
    internal class ResponseConsumer(TestDbContext dbContext) : IConsumer<UserResponseDto>
    {
        public Task Consume(ConsumeContext<UserResponseDto> context)
        {

            var score = MarkerService.Mark(dbContext, context.Message.Answers!);

            var userScore = new Score
            {
                Id = Guid.NewGuid(),
                UserId = context.Message.UserId,
                ScoreValue = score,
                CreatedAt = DateTime.UtcNow
            };

            dbContext.Scores.Add(userScore);

            var response = new Entities.Response
            {
                UserId = context.Message.UserId,
                SelectedChoices = context.Message.Answers!
                    .Select(a => new SelectedChoice
                    {
                        QuestionId = a.QuestionId,
                        ChoiceId = a.ChoiceId,
                        IsChoiceCorrect = dbContext.Choices
                            .Where(c => c.Id == a.ChoiceId)
                            .Select(c => c.IsCorrect)
                            .FirstOrDefault()
                    })
                    .ToList()
            };

            dbContext.Responses.Add(response);

            dbContext.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
