using System.Net.Mail;
using System.Net;
using ElectricityBillingSystemDemo.Interfaces;

namespace ElectricityBillingSystemDemo.Repositories
{
    public class EmailRepository
    {
        public async Task SendEmail(string to, string subject, string body)
        {
            using (var client = new SmtpClient())
            {
                var credentials = new System.Net.NetworkCredential
                {
                    UserName = "gtprithvi2001@gmail.com", 
                    Password = "ujjr ozkw reww snpf" 
                };

                client.Credentials = credentials;
                client.Host = "smtp.gmail.com"; 
                client.Port = 587; 
                client.EnableSsl = true; 

                var message = new MailMessage
                {
                    From = new MailAddress("gtprithvi2001@gmail.com") 
                };
                message.To.Add(to);
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;

                await client.SendMailAsync(message);
            }
        }

    }
}
