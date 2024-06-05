using Microsoft.Extensions.Logging;
using RSAllies.Test.Contracts.Requests;

namespace RSAllies.Test.Extensions
{
    public static partial class Logging
    {
        [LoggerMessage(LogLevel.Information, "Question created")]
        public static partial void LogQuestionCreated(this ILogger logger, [LogProperties] CreateQuestionDto question);

        [LoggerMessage(LogLevel.Information, "Question edited")]
        public static partial void LogQuestionEdited(this ILogger logger, [LogProperties] CreateQuestionDto question);
    }
}
