﻿namespace ApiTemplate.Commands.SlideCommands
{
    using ViewModels.SlideViewModels;
    using Boxed.AspNetCore;
    public interface IPutSlideCommand : IAsyncCommand<int, SaveSlide>
    {
    }
}
