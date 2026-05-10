using Resend;

namespace Tune.site
{
    public class EmailService  // renamed to avoid conflict with Resend namespace
    {
        private const string KEY = "re_5zRaWTAo_8UxgMxxJCV2tjmb1G6VAWVck";

        public static async Task SendMessage(string toEmail, int num)  // accept email as parameter
        {
            IResend resend = ResendClient.Create(KEY);

            var resp = await resend.EmailSendAsync(new EmailMessage()
            {
                From = "onboarding@resend.dev",
                To = "michaelvinnik08@gmail.com", // always goes to you
                Subject = $"Password Reset Code for {toEmail}", // shows who requested it
                HtmlBody = $"<p>Reset code for <strong>{toEmail}</strong>: <strong>{num}</strong></p>"
            });
        }
    }
}
