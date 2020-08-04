namespace ApiTemplate.Commands.SlideCommands
{
    using Constants;
    using Repositories;
    using ViewModels.SlideContentViewModels;
    using ViewModels.SlideViewModels;
    using Boxed.Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Linq;

    public class PostSlideCommand : IPostSlideCommand
    {
        private readonly ISlideRepository _slideRepository;
        private readonly IMapper<Models.Slide, Slide> _slideToSlideMapper;
        private readonly IMapper<SaveSlide, Models.Slide> _saveSlideToSlideMapper;
        
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PostSlideCommand(
            ISlideRepository slideRepository,
            IMapper<Models.Slide, Slide> slideToSlideMapper,
            IMapper<SaveSlide, Models.Slide> saveSlideToSlideMapper,
            IMapper<SaveSlideContent, Models.SlideContent> saveSlideContentToSlideContentMapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _slideRepository = slideRepository;
            _slideToSlideMapper = slideToSlideMapper;
            _saveSlideToSlideMapper = saveSlideToSlideMapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> ExecuteAsync(SaveSlide saveSlide, CancellationToken cancellationToken)
        {
            var slide = _saveSlideToSlideMapper.Map(saveSlide);

            //var user = _httpContextAccessor.HttpContext.User;
            //if (user == null)
            //    return new NotFoundResult();

            //var claims = user.Claims.ToList();
            //if (claims.Count < 1)
            //    return new NotFoundResult();
            //// Lấy Id của người dùng
            //var userId = claims.FirstOrDefault(claimRecord => claimRecord.Type == "sub")?.Value;

            //slide.CreatedBy = userId;
            //slide.ModifiedBy = userId;

            slide = await _slideRepository.Add(slide, cancellationToken);
            var slideViewModel = _slideToSlideMapper.Map(slide);

            return new OkObjectResult(slideViewModel);
        }
    }
}
