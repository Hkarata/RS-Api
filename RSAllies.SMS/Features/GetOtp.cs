using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using RSAllies.SMS.Contracts.Requests;
using RSAllies.SMS.Services;

namespace RSAllies.SMS.Features;

public class GetOtp(OtpService otpService) : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/otp/{phone}", async (string phone) =>
        {
            var request = new OtpRequest
            {
                appId = 1917,
                msisdn = phone
            };

            var response = await otpService.RequestOtp(request);

            return Results.Ok(response);
        });
    }
}