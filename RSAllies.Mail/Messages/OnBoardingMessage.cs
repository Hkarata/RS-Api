namespace RSAllies.Mail.Messages
{
    internal static class OnBoardingMessage
    {
        public static string GetOnBoardingMessage(string name)
        {
            string content = "Driver-Centric Theoretical Training System\n\n" +
                 "Welcome, {{name}}\n" +
                 "Thank you for creating an account with the Driver Centric Theoretical Training System. " +
                 "We're excited to help you on your journey to becoming a safe and knowledgeable driver.\n\n" +
                 "Next Steps:\n" +
                 "- Reserve your spot at the nearest center\n" +
                 "- Show up on the designated day to take the test\n\n" +
                 "Start by logging into your account and reserve the relevant spot.\n\n" +
                 "Mfumo wa Kupima Nadharia ya Udereva\n\n" +
                 "Karibu, {{name}}\n" +
                 "Asante kwa kuunda akaunti na Mfumo wa Mafunzo ya Nadharia ya Dereva-Kuu. " +
                 "Tuna furaha kusaidia katika safari yako ya kuwa dereva salama na mwenye maarifa.\n\n" +
                 "Hatua zinazofata:\n" +
                 "- Hifadhi nafasi yako katika kituo kilicho karibu nawe\n" +
                 "- Tokea siku husika kufanya mtihani\n\n" +
                 "Anza kwa kuingia kwenye akaunti yako na hifadhi nafasi husika.";

            return content.Replace("{{name}}", name);
        }
    }
}
