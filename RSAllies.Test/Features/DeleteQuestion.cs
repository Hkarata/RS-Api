using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RSAllies.Shared.HelperTypes;
using RSAllies.Test.Data;
using RSAllies.Test.Features;

namespace RSAllies.Test.Features
{
    internal class DeleteQuestion
    {
        internal class Query : IRequest<Result>
        {
            public Guid QuestionId { get; set; }
        }

        internal sealed class Handler(TestDbContext context) : IRequestHandler<Query, Result>
        {
            public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
            {
                var question = await context.Questions
                    .Where(qn => qn.Id == request.QuestionId)
                    .SingleOrDefaultAsync(cancellationToken);

                if (question == null)
                {
                    return Result.Failure(Error.ConditionNotMet);
                }

                question.IsDeleted = true;
                await context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
    }
}

public class DeleteQuestionEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/question/{questionId:guid}", async (Guid questionId, ISender sender) =>
        {
            var request = new DeleteQuestion.Query { QuestionId = questionId };
            var result = await sender.Send(request);
            return result.IsSuccess ? Results.Ok(result) : Results.Ok(result.Error);
        });
    }
}
