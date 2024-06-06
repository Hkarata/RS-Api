using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using RSAllies.Shared.HelperTypes;
using RSAllies.Test.Contracts.Requests;
using RSAllies.Test.Data;
using RSAllies.Test.Entities;
using RSAllies.Test.Extensions;
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
                    Scenario = request.Scenario,
                    ImageUrl = request.ImageUrl,
                    QuestionText = request.Question,
                    Choices = request.Choices.Select(choice => new Choice
                    {
                        ChoiceText = choice.ChoiceText,
                        IsCorrect = choice.IsAnswer
                    }).ToList(),
                    IsEnglish = request.IsEnglish
                };

                context.Questions.Add(question);

                await context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
    }
}


public class CreateQuestionEndPoint(ILogger<CreateQuestionEndPoint> logger) : ICarterModule
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

            logger.LogQuestionCreated(question);

            return result.IsFailure ? Results.Ok(result.Error) : Results.Ok(result);

        })
            .Produces<Result>()
            .WithTags("Question");
    }
}