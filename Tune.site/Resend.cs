using System.Net.Mail;
using Resend;

namespace Tune.site
{
    public class Resend
    {
        private const string KEY = "re_5zRaWTAo_8UxgMxxJCV2tjmb1G6VAWVck";

        public static async Task message(int num)
        {
            IResend resend = ResendClient.Create(KEY);
            var resp = await resend.EmailSendAsync(new EmailMessage()
            {
                From = "onboarding@resend.dev",
                To = "michaelvinnik08@gmail.com",
                Subject = "Reset Your Password",
                HtmlBody = @"@num"

            });
        }
    }
}
