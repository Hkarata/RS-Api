using Microsoft.EntityFrameworkCore;
using RSAllies.Test.Contracts.Requests;
using RSAllies.Test.Data;

namespace RSAllies.Test.Services
{
    internal static class MarkerService
    {
        public static int Mark(TestDbContext context, List<AnswerDto> answers)
        {
            var markingScheme = context.Questions
                .Include(q => q.Choices)
                .Select(q => new
                {
                    q.Id,
                    CorrectChoiceId = q.Choices
                        .Where(c => c.IsCorrect)
                        .Select(c => c.Id)
                        .FirstOrDefault()
                })
                .ToDictionary(q => q.Id, q => q.CorrectChoiceId);

            var score = 0;

            foreach (var answer in answers)
            {
                if (markingScheme.TryGetValue(answer.QuestionId, out var correctChoiceId) && correctChoiceId == answer.ChoiceId)
                {
                    score++;
                }
            }

            return score;
        }
    }
}
