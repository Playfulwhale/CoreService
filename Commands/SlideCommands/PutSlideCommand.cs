namespace ApiTemplate.Commands.SlideCommands
{
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Mvc;
    using Repositories;
    using System.Threading;
    using System.Threading.Tasks;
    using ViewModels.SlideViewModels;

    public class PutSlideCommand : IPutSlideCommand
    {
        private readonly ISlideRepository _slideRepository;
        private readonly IMapper<Models.Slide, Slide> _slideToSlideMapper;
        private readonly IMapper<SaveSlide, Models.Slide> _saveSlideToSlideMapper;

        public PutSlideCommand(
            ISlideRepository slideRepository,
            IMapper<Models.Slide, Slide> slideToSlideMapper,
            IMapper<SaveSlide, Models.Slide> saveSlideToSlideMapper)
        {
            _slideRepository = slideRepository;
            _slideToSlideMapper = slideToSlideMapper;
            _saveSlideToSlideMapper = saveSlideToSlideMapper;
        }

        public async Task<IActionResult> ExecuteAsync(int slideId, SaveSlide saveSlide, CancellationToken cancellationToken)
        {
            var slide = await _slideRepository.Get(slideId, cancellationToken);
            if (slide == null)
            {
                return new NotFoundResult();
            }
            _saveSlideToSlideMapper.Map(saveSlide, slide);

            //var user = _httpContextAccessor.HttpContext.User;
            //if (user == null)
            //    return new NotFoundResult();

            //var claims = user.Claims.ToList();
            //if (claims.Count < 1)
            //    return new NotFoundResult();
            //// Lấy Id của người dùng
            //var userId = claims.FirstOrDefault(claimRecord => claimRecord.Type == "sub")?.Value;

            //slide.ModifiedBy = userId;

            slide = await _slideRepository.Update(slide, cancellationToken);
            var slideViewModel = _slideToSlideMapper.Map(slide);

            return new OkObjectResult(slideViewModel);
        }
    }
}
