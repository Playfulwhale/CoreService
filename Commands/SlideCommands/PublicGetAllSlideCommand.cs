namespace ApiTemplate.Commands.SlideCommands
{
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Repositories;
    using System.Threading;
    using System.Threading.Tasks;
    using ViewModels.SlideViewModels;

    public class PublicGetAllSlideCommand : IPublicIGetAllSlideCommand
    {
        private readonly ISlideRepository _slideRepository;
        private readonly IMapper<Models.Slide, PublicSlide> _slideToPublicSlideMapper;

        public PublicGetAllSlideCommand(
            ISlideRepository slideRepository,
            IMapper<Models.Slide, PublicSlide> slideToPublicSlideMapper)
        {
            _slideRepository = slideRepository;
            _slideToPublicSlideMapper = slideToPublicSlideMapper;
        }

        public async Task<IActionResult> ExecuteAsync(CancellationToken cancellationToken)
        {
            var listPublicSlide = await _slideRepository.PublicGetAll(cancellationToken);

            if (listPublicSlide == null)
                return new NoContentResult();

            // Map dữ liệu sang PublicSlide
            var publicSlideViewModels = _slideToPublicSlideMapper.MapList(listPublicSlide);

            return new OkObjectResult(publicSlideViewModels);
        }
    }
}