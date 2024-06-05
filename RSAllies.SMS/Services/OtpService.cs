using Newtonsoft.Json;
using RSAllies.Shared.HelperTypes;
using RSAllies.SMS.Contracts.Requests;
using RSAllies.SMS.Contracts.Responses;
using System.Net.Http.Json;

namespace RSAllies.SMS.Services;

public class OtpService(HttpClient httpClient)
{
    public async Task<Result<OtpSuccess>> RequestOtp(OtpRequest otpRequest)
    {
        var response = await httpClient.PostAsJsonAsync("/v1/request", otpRequest);
        var content = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            var otpSuccess = JsonConvert.DeserializeObject<OtpSuccess>(content);
            return otpSuccess;
        }
        var otpError = JsonConvert.DeserializeObject<OtpError>(content);
        return Result.Failure<OtpSuccess>(
            new Error("OtpRequest.Failed", "Otp Request Failed"));
    }

    public async Task<OtpResponse> VerifyOtp(OtpVerification otpVerification)
    {
        var response = await httpClient.PostAsJsonAsync("/v1/verify", otpVerification);
        var content = await response.Content.ReadAsStringAsync();
        var otpResponse = JsonConvert.DeserializeObject<OtpResponse>(content);
        return otpResponse!;
    }
}