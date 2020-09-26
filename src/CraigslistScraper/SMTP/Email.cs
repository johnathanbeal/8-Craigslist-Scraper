using System;
using System.Threading.Tasks;
using System.Net.Mail;
//using CraigslistScraper.Credentials;
using CraigslistScraper.Craigslist;


namespace CraigslistScraper.SMTP
{
    public class Email
    {
        public async Task<EmailStatus> SendEmail(string htmlBody)
        {
            //EmailAuthenticationInfo emailAuthenticationInfo = new EmailAuthenticationInfo();
            try
            {
                string to = "johnathanbeal@gmail.com";
                string from = "jbeal.i360@gmail.com";

                MailMessage mail = new MailMessage(from, to);
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.Subject = "Daily Apartments Notification";
                

                mail.Body = htmlBody;
                mail.IsBodyHtml = true;

                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.Port = 587;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential(
                    Environment.GetEnvironmentVariable("SENDER_EMAIL"), 
                    Environment.GetEnvironmentVariable("SENDER_PASSWORD"));

                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

                Console.WriteLine("email sent");
                var emailStatus = new EmailStatus();
                emailStatus.WasSent = true;
                emailStatus.Message = "email sent";
                return await emailStatus;
            }
            catch (Exception ex)
            {
                var emailFailStatus = new EmailStatus();
                emailFailStatus.WasSent = true;
                emailFailStatus.Message = "email was not sent, exception is:" + ex.ToString();

                return await emailFailStatus;
                
            }
        }
    }
}

public class EmailStatus
{
    public bool WasSent { get; set; }
    public string Message { get; set; }
}