namespace ApiTemplate.Repositories
{
    using Data;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class ContactUsRepository : IContactUsRepository
    {
        private readonly DataContext _context;

        public ContactUsRepository(DataContext context)
        {
            _context = context;
        }

        public Task<ContactUs> Add(ContactUs contactUs, CancellationToken cancellationToken)
        {
            _context.ContactUs.Add(contactUs);
            _context.SaveChanges();
            return Task.FromResult(contactUs);
        }

        public Task<ContactUs> Update(ContactUs contactUs, CancellationToken cancellationToken)
        {
            var existingContactUs = _context.ContactUs.FirstOrDefault(x => x.Id == contactUs.Id);
            if (existingContactUs == null) return Task.FromResult((ContactUs) null);
            existingContactUs.Title = contactUs.Title;
            existingContactUs.FirstName = contactUs.FirstName;
            existingContactUs.LastName = contactUs.LastName;
            existingContactUs.Email = contactUs.Email;
            existingContactUs.Phone = contactUs.Phone;
            existingContactUs.Message = contactUs.Message;
            existingContactUs.Status = contactUs.Status;
            existingContactUs.ModifiedBy = contactUs.ModifiedBy;
            existingContactUs.ModifiedAt = contactUs.ModifiedAt;
            _context.SaveChanges();
            return Task.FromResult(existingContactUs);
        }

        public Task Delete(ContactUs contactUs, CancellationToken cancellationToken)
        {
            if (_context.ContactUs.Contains(contactUs))
            {
                _context.ContactUs.Remove(contactUs);
                _context.SaveChanges();
            }
            return Task.CompletedTask;
        }

        public Task<ContactUs> Get(int id, CancellationToken cancellationToken)
        {
            var contactUs = _context.ContactUs.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(contactUs);
        }

        public Task<List<ContactUs>> GetAll(CancellationToken cancellationToken)
        {
            var list = _context.ContactUs.ToList();
            return Task.FromResult(list);
        }
    }
}