using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using RSAllies.Shared.HelperTypes;
using RSAllies.Test.Contracts.Requests;
using RSAllies.Test.Data;
using RSAllies.Test.Entities;
using RSAllies.Test.Features;

namespace RSAllies.Test.Features
{
    internal abstract class CreateQuestion
    {
        internal class Query : IRequest<Result>
        {
            public string Scenario { get; set; } = string.Empty;
            public string ImageUrl { get; set; } = string.Empty;
            public string Question { get; set; } = string.Empty;
            public bool IsEnglish { get; set; }
            public required List<CreateChoiceDto> Choices { get; set; }
        }

        internal sealed class Handler(TestDbContext context) : IRequestHandler<Query, Result>
        {
            public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
            {
                var question = new Question
                {
                    Id = Guid.NewGuid(),
                    Scenario = request.Scenario,
                    ImageUrl = request.ImageUrl,
                    QuestionText = request.Question,
                    Choices = request.Choices.Select(choice => new Choice
                    {
                        Id = Guid.NewGuid(),
                        ChoiceText = choice.ChoiceText,
                        IsCorrect = choice.IsAnswer
                    }).ToList(),
                    IsEnglish = request.IsEnglish
                };

                var answer = question.Choices
                    .Where(c => c.IsCorrect)
                    .Select(a => new Answer
                    {
                        Id = Guid.NewGuid(),
                        ChoiceId = a.Id,
                        QuestionId = question.Id
                    })
                    .SingleOrDefault();

                if (answer == null)
                {
                    return Result.Failure(Error.ConditionNotMet);
                }

                context.Questions.Add(question);

                context.Answers.Add(answer);

                await context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
    }
}


public class CreateQuestionEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/question", async (CreateQuestionDto question, ISender sender) =>
        {
            var request = new CreateQuestion.Query
            {
                Scenario = question.Scenario!,
                ImageUrl = question.ImageUrl!,
                Question = question.Question,
                IsEnglish = question.IsEnglish,
                Choices = question.Choices
            };

            var result = await sender.Send(request);

            return result.IsFailure ? Results.Ok(result.Error) : Results.Ok(result);

        })
            .Produces<Result>()
            .WithTags("Question");
    }
}