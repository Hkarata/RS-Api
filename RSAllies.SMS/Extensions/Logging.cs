using Microsoft.Extensions.Logging;
using RSAllies.SMS.Contracts.Requests;
using RSAllies.SMS.Contracts.Responses;

namespace RSAllies.SMS.Extensions;

public static partial class Logging
{
    [LoggerMessage(LogLevel.Information, "Sms request sent: ")]
    public static partial void
        LogMessageRequest(this ILogger logger, [LogProperties] Sms sms);

    [LoggerMessage(LogLevel.Error, "Sms request failed: ")]
    public static partial void
        LogMessageRequestFailed(this ILogger logger, [LogProperties] SmsError smsError);

    [LoggerMessage(LogLevel.Information, "Sms request sent: ")]
    public static partial void
        LogMessageRequestSuccess(this ILogger logger, [LogProperties] SmsSuccess smsSuccess);

}