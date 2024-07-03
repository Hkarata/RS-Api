using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RSAllies.Shared.HelperTypes;
using RSAllies.Test.Contracts.Requests;
using RSAllies.Test.Data;
using RSAllies.Test.Entities;
using RSAllies.Test.Features;

namespace RSAllies.Test.Features
{
    internal abstract class EditQuestion
    {
        internal class Query : IRequest<Result>
        {
            public Guid QuestionId { get; set; }
            public string Scenario { get; set; } = string.Empty;
            public string Question { get; set; } = string.Empty;
            public string ImageUrl { get; set; } = string.Empty;
            public bool IsEnglish { get; set; }
            public required List<CreateChoiceDto> Choices { get; set; }
        }

        internal sealed class Handler(TestDbContext context) : IRequestHandler<Query, Result>
        {
            public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
            {
                var question = await context.Questions
                    .Where(q => q.Id == request.QuestionId && !q.IsDeleted)
                    .Include(q => q.Choices)
                    .SingleOrDefaultAsync(cancellationToken);

                if (question is null)
                {
                    return Result.Failure(new Error("EditQuestion.NonExistent", "The specified question does not exist"));
                }

                question.Scenario = request.Scenario;
                question.QuestionText = request.Question;
                question.IsEnglish = request.IsEnglish;
                question.ImageUrl = request.ImageUrl;

                question.Choices = request.Choices.Select(c => new Choice
                {
                    ChoiceText = c.ChoiceText,
                    IsCorrect = c.IsAnswer
                }).ToList();

                var answer = await context.Answers
                    .Where(a => a.QuestionId == question.Id)
                    .SingleOrDefaultAsync(cancellationToken);

                if (answer is not null)
                {
                    answer.ChoiceId = question.Choices.Single(c => c.IsCorrect).Id;
                }

                await context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
    }
}


public class EditQuestionEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/question/{questionId:guid}", async (Guid questionId, CreateQuestionDto question, ISender sender) =>
        {
            var request = new EditQuestion.Query
            {
                QuestionId = questionId,
                Scenario = question.Scenario!,
                Question = question.Question,
                ImageUrl = question.ImageUrl!,
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