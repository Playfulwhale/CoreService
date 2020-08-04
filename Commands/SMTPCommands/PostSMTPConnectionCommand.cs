namespace ApiTemplate.Commands.SMTPCommands
{
    using ViewModels.SMTPViewModels;
    using System.Net.Mail;
    using Microsoft.AspNetCore.Mvc;

    public class PostSmtpConnectionCommand : IPostSmtpConnectionCommand
    {
        private readonly SmtpClient _smtpClient;
        private readonly MailMessage _mail;

        public PostSmtpConnectionCommand()
        {
            _mail = new MailMessage();
            _smtpClient = new SmtpClient();
        }
        public IActionResult Execute(SmtpConnection smtpConnection)
        {
            try {
                _mail.From = new MailAddress("connection@test.com", "");
                _mail.Subject = "";
                _mail.Body = "";
                _mail.IsBodyHtml = true;
                _mail.To.Add("to@email.com");

                _smtpClient.Host = smtpConnection.Host;
                _smtpClient.Port = smtpConnection.Port;
                _smtpClient.EnableSsl = true;
                _smtpClient.Credentials = new System.Net.NetworkCredential(smtpConnection.Username, smtpConnection.Password);
                _smtpClient.Send(_mail);
            } catch { return new JsonResult(false); }

            return new JsonResult(true);
        }
    }
}
