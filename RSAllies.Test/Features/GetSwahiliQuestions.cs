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
    internal class GetSwahiliQuestions
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
                    .Where(q => !q.IsEnglish && !q.IsDeleted)
                    .Include(q => q.Choices)
                    .Select(q => new QuestionDto
                    {
                        Id = q.Id,
                        Scenario = q.Scenario!,
                        ImageUrl = q.ImageUrl!,
                        Question = q.QuestionText,
                        Choices = q.Choices.Select(c => new ChoiceDto { Id = c.Id, ChoiceText = c.ChoiceText }).ToList()
                    })
                    .ToListAsync(cancellationToken);

                if (questions.Count == 0)
                {
                    return Result.Failure<List<QuestionDto>>(new Error("GetSwahiliQuestions.None",
                        "There are no English questions available."
                        ));
                }

                return questions;
            }
        }
    }
}

public class GetSwahiliQuestionsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/questions/swahili", async (ISender sender) =>
        {
            var request = new GetSwahiliQuestions.Query();
            var result = await sender.Send(request);
            return result.IsFailure ? Results.Ok(result.Error) : Results.Ok(result);
        })
            .Produces<Result<List<QuestionDto>>>()
            .WithTags("Questions");
    }
}