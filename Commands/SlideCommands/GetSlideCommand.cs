namespace ApiTemplate.Commands.SlideCommands
{
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Repositories;
    using System.Threading;
    using System.Threading.Tasks;
    using ViewModels.SlideViewModels;

    public class GetSlideCommand : IGetSlideCommand
    {
        private readonly ISlideRepository _slideRepository;
        private readonly IMapper<Models.Slide, Slide> _slideMapper;

        public GetSlideCommand(
            ISlideRepository slideRepository,
            IMapper<Models.Slide, Slide> slideMapper)
        {
            _slideRepository = slideRepository;
            _slideMapper = slideMapper;
        }

        public async Task<IActionResult> ExecuteAsync(int id, CancellationToken cancellationToken)
        {
            var slide = await _slideRepository.Get(id, cancellationToken);
            if (slide == null)
            {
                return new NoContentResult();
            }
            var slideViewModels = _slideMapper.Map(slide);

            return new OkObjectResult(slideViewModels);
        }
    }
}