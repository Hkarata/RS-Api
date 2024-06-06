using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RSAllies.Shared.HelperTypes;
using RSAllies.Test.Contracts.Responses;
using RSAllies.Test.Data;
using RSAllies.Test.Features;

namespace RSAllies.Test.Features
{
    internal class GetEnglishQuestions
    {
        internal class Query : IRequest<Result<List<QuestionDto>>>
        {
        }

        internal sealed class Handler(TestDbContext context) : IRequestHandler<Query, Result<List<QuestionDto>>>
        {
            public async Task<Result<List<QuestionDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var questions = await context.Questions
                    .AsNoTracking()
                    .Where(q => q.IsEnglish && !q.IsDeleted)
                    .Include(q => q.Choices)
                    .Select(q => new QuestionDto
                    {
                        Scenario = q.Scenario!,
                        ImageUrl = q.ImageUrl!,
                        Question = q.QuestionText,
                        Choices = q.Choices.Select(c => new ChoiceDto { ChoiceText = c.ChoiceText }).ToList()
                    })
                    .ToListAsync(cancellationToken);

                if (questions.Count == 0)
                {
                    return Result.Failure<List<QuestionDto>>(new Error("GetEnglishQuestions.None",
                        "There are no English questions available."
                        ));
                }

                return questions;
            }
        }
    }
}

public class GetEnglishQuestionsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/questions/english", async (ISender sender) =>
        {
            var request = new GetEnglishQuestions.Query();
            var result = await sender.Send(request);
            return result.IsFailure ? Results.Ok(result.Error) : Results.Ok(result);
        })
            .Produces<Result<List<QuestionDto>>>()
            .WithTags("Questions");
    }
}