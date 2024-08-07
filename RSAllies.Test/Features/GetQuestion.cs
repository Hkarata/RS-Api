﻿using Carter;
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
    internal class GetQuestion
    {
        internal class Query : IRequest<Result<AllQuestionDto>>
        {
            public Guid Id { get; set; }
        }

        internal sealed class Handler(TestDbContext context) : IRequestHandler<Query, Result<AllQuestionDto>>
        {
            public async Task<Result<AllQuestionDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var question = await context.Questions
                    .Include(q => q.Choices)
                    .Where(q => q.Id == request.Id && !q.IsDeleted)
                    .Select(qn => new AllQuestionDto
                    {
                        Id = qn.Id,
                        Scenario = qn.Scenario!,
                        ImageUrl = qn.ImageUrl!,
                        QuestionText = qn.QuestionText,
                        Choices = qn.Choices.Select(c => new AllChoiceDto
                        {
                            ChoiceText = c.ChoiceText,
                            IsCorrect = c.IsCorrect
                        }).ToList()
                    })
                    .SingleOrDefaultAsync(cancellationToken);

                if (question is null)
                {
                    return Result.Failure<AllQuestionDto>(new Error("GetQuestion.NonExistent", "The specified question does not exist"));
                }

                return Result.Success(question);
            }
        }
    }
}


public class GetQuestionEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/question/{questionId:guid}", async (Guid questionId, ISender sender) =>
        {
            var request = new GetQuestion.Query { Id = questionId };
            var result = await sender.Send(request);
            return result.IsSuccess ? Results.Ok(result) : Results.Ok(result.Error);
        });
    }
}