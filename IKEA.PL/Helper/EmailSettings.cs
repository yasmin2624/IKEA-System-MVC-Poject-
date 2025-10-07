using System.Net;
using System.Net.Mail;

namespace IKEA.PL.Helper
{
    public static class EmailSettings
    {
        public static void SendEmail (Email email)
        {
            var Client = new SmtpClient("smtp.gmail.com", 587);
            Client.EnableSsl = true;
            Client.Credentials = new NetworkCredential("yasminhwork2624@gmail.com", "vzcavrefqyzblrcz");
            Client.Send("yasminhwork2624@gmail.com", email.To, email.Subject,email.Body);
        }
    }
}
