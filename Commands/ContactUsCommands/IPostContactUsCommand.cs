namespace ApiTemplate.Commands.ContactUsCommands
{
    using Boxed.AspNetCore;
    using ViewModels.ContactUsViewModels;

    public interface IPostContactUsCommand : IAsyncCommand<SaveContactUs>
    {
    }
}