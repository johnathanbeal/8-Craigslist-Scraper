using System;
using System.Threading.Tasks;
using System.Net.Mail;
using CraigslistScraper.Credentials;
using CraigslistScraper.Craigslist;


namespace HyperScrape.SMTP
{
    public class Email
    {
        public async Task SendEmail()
        {
            EmailAuthenticationInfo emailAuthenticationInfo = new EmailAuthenticationInfo();
            SearchApartments searchApartments = new SearchApartments();

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("jbeal.i360@gmail.com");

                mail.To.Add("johnathanbeal@gmail.com");
                mail.Subject = "Daily Apartments Notification";
                var apartments = await searchApartments.SearchApartmentsAsync(new string[] { "Arlington", "750" });
                string _apartments = string.Join(Environment.NewLine, apartments.ToArray());

                mail.Body = _apartments;
                mail.IsBodyHtml = true;

                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.Port = 587;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential(emailAuthenticationInfo.Username, emailAuthenticationInfo.Password);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

                Console.WriteLine("email sent");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}