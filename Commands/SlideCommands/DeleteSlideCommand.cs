namespace ApiTemplate.Commands.SlideCommands
{
    using System.Threading;
    using System.Threading.Tasks;
    using Repositories;
    using Microsoft.AspNetCore.Mvc;
    public class DeleteSlideCommand : IDeleteSlideCommand
    {
        private readonly ISlideRepository _slideRepository;

        public DeleteSlideCommand(ISlideRepository slideRepository) =>
            _slideRepository = slideRepository;

        public async Task<IActionResult> ExecuteAsync(int slideId, CancellationToken cancellationToken)
        {
            var slide = await _slideRepository.Get(slideId, cancellationToken);
            if (slide == null)
            {
                return new NotFoundResult();
            }

            await _slideRepository.Delete(slide, cancellationToken);
            return new NoContentResult();
        }
    }
}
