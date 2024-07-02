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
    internal class GetAllQuestions
    {
        internal class Query : IRequest<Result<List<AllQuestionDto>>>
        {
        }

        internal sealed class Handler(TestDbContext context) : IRequestHandler<Query, Result<List<AllQuestionDto>>>
        {
            public async Task<Result<List<AllQuestionDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var questions = await context.Questions
                    .Include(q => q.Choices)
                    .Where(q => !q.IsDeleted)
                    .Select(q => new AllQuestionDto
                    {
                        Id = q.Id,
                        Scenario = q.Scenario!,
                        ImageUrl = q.ImageUrl!,
                        QuestionText = q.QuestionText,
                        IsEnglish = q.IsEnglish,
                        Choices = q.Choices.Select(c => new AllChoiceDto
                        {
                            ChoiceText = c.ChoiceText,
                            IsCorrect = c.IsCorrect
                        }).ToList()
                    })
                    .ToListAsync(cancellationToken);

                if (questions.Count == 0)
                {
                    return Result.Failure<List<AllQuestionDto>>(Error.NullValue);
                }

                return questions;
            }
        }
    }
}

public class GetAllQuestionsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/questions", async (ISender sender) =>
        {
            var request = new GetAllQuestions.Query();
            var result = await sender.Send(request);
            return result.IsSuccess ? Results.Ok(result) : Results.Ok(result.Error);
        });
    }
}