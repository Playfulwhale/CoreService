namespace ApiTemplate.Repositories
{
    using Data;
    using Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class SlideRepository : ISlideRepository
    {
        private readonly DataContext _context;

        public SlideRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Slide> Add(Slide slide, CancellationToken cancellationToken)
        {
            await _context.Slides.AddAsync(slide, cancellationToken);
            foreach(var item in slide.SlideContents)
            {
                item.SlideId = slide.Id;
            }
            await _context.SlideContents.AddRangeAsync(slide.SlideContents, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return slide;
        }

        public async Task Delete(Slide slide, CancellationToken cancellationToken)
        {
            _context.Slides.Remove(slide);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Slide> Get(int id, CancellationToken cancellationToken)
        {
            var slide = await _context.Slides.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            var slideContents = await _context.SlideContents.Where(x => x.SlideId == id).ToListAsync(cancellationToken);
            slide.SlideContents = slideContents;
            return slide;
        }

        public async Task<ICollection<Slide>> GetAll(CancellationToken cancellationToken)
        {
            var slides = await _context.Slides.ToListAsync(cancellationToken);
            foreach(var slide in slides)
            {
                var slideContents = await _context.SlideContents.Where(x => x.SlideId == slide.Id).ToListAsync(cancellationToken);
                slide.SlideContents = slideContents;
            }
            return slides;
        }

        public async Task<ICollection<Slide>> PublicGetAll(CancellationToken cancellationToken)
        {
            return await _context.Slides.Include(x => x.SlideContents)
                                        .Where(x => x.Status).OrderBy(x => x.SortOrder)
                                        .ToListAsync(cancellationToken);
        }

        public async Task<Slide> Update(Slide slide, CancellationToken cancellationToken)
        {
            var slideExist = await _context.Slides.FirstOrDefaultAsync(x => x.Id == slide.Id, cancellationToken);
            if (slideExist != null)
            {
                slideExist.LinkType = slide.LinkType;
                slideExist.Link = slide.Link;
                slideExist.ImageUrl = slide.ImageUrl;
                slideExist.OpenNewTab = slide.OpenNewTab;
                slideExist.SortOrder = slide.SortOrder;
                slideExist.Status = slide.Status;
                _context.Update(slideExist);
            }            

            foreach(var item in slide.SlideContents)
            {
                var slideContent = await _context.SlideContents.FirstOrDefaultAsync(x => x.SlideId == slide.Id & x.LanguageCode == item.LanguageCode, cancellationToken);
                if(slideContent == null)
                {
                    _context.SlideContents.Add(item);
                }
                else
                {
                    slideContent.Title = item.Title;
                    slideContent.Caption = item.Caption;
                    _context.Update(slideContent);
                }
               
            }
            await _context.SaveChangesAsync(cancellationToken);
            return slide;
        }
    }
}
