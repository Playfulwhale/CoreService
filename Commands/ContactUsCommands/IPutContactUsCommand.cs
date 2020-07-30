namespace ApiTemplate.Commands.ContactUsCommands
{
    using Boxed.AspNetCore;

    public interface IPutContactUsCommand : IAsyncCommand<int, string>
    {
    }
}