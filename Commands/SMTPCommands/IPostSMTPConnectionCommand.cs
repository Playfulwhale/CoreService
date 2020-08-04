namespace ApiTemplate.Commands.SMTPCommands
{
    using Boxed.AspNetCore;
    using ViewModels.SMTPViewModels;

    public interface IPostSmtpConnectionCommand : ICommand<SmtpConnection>
    {
    }
}
