namespace RSAllies.SMS.Messages;

public static class OnBoardingMessage
{
    public static string GetOnBoardingMessage()
    {
        const string english = "Welcome to our Driver Training! \ud83d\ude97 Book and attempt your exam at [exam portal URL]. Need help? Contact us at [Support Email/Phone Number].";
        const string swahili = "";
        return english + swahili;
    }
}