namespace ApiTemplate.Repositories
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Models;

    public class ZoneRepository : IZoneRepository
    {
        private readonly DataContext _context;
        public ZoneRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Zone> Add(Zone zone, CancellationToken cancellationToken)
        {

            await _context.Zones.AddAsync(zone, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return zone;
        }

        public async Task Delete(Zone zone, CancellationToken cancellationToken)
        {
            _context.Zones.Remove(zone);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Zone> Get(int zoneId, CancellationToken cancellationToken)
        {
            var zone = await _context.Zones.FindAsync(zoneId);
            return zone;
        }

        public async Task<ICollection<Zone>> GetPage(int page, int count, CancellationToken cancellationToken)
        {
            var zones = await _context.Zones.ToListAsync(cancellationToken);
            var pageZones = zones
                .Skip(count * (page - 1))
                .Take(count)
                .ToList();
            if (pageZones.Count == 0)
            {
                pageZones = null;
            }

            return pageZones;
        }

        public async Task<(int totalCount, int totalPages)> GetTotalPages(int count, CancellationToken cancellationToken)
        {
            var zones = await _context.Zones.ToListAsync(cancellationToken);
            var totalPages = (int)Math.Ceiling(zones.Count / (double)count);
            return (zones.Count, totalPages);
        }

        public async Task<(int totalCount, int totalPages)> GetCountryIdTotalPages(int countryId, int count, CancellationToken cancellationToken)
        {
            var zoneList = await _context.Zones.Include(c => c.Country).Where(c => c.CountryId == countryId).ToListAsync(cancellationToken);
            var totalPages = (int)Math.Ceiling(zoneList.Count / (double)count);
            return (zoneList.Count, totalPages);
        }

        public async Task<Zone> Update(Zone zone, CancellationToken cancellationToken)
        {
            var existingZone = await _context.Zones.FirstOrDefaultAsync(x => x.Id == zone.Id, cancellationToken);

            if (existingZone != null)
            {
                existingZone.Title = zone.Title;
                existingZone.Code = zone.Code;
                existingZone.CountryId = zone.CountryId;

                _context.Entry(existingZone).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync(cancellationToken);
            return zone;
        }

        public async Task<Zone> ToggleStatus(int zoneId, CancellationToken cancellationToken)
        {
            var existingZone = await _context.Zones.FindAsync(zoneId);
            existingZone.Active = !existingZone.Active || !existingZone.Active;
            _context.Entry(existingZone).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);

            return existingZone;
        }

        public async Task<ICollection<Zone>> GetCountryIdPage(int countryId, int page, int count, CancellationToken cancellationToken)
        {
            var zoneList = await _context.Zones.Include(c => c.Country).Where(c => c.CountryId == countryId).ToListAsync(cancellationToken);

            var pageZoneList = zoneList
                .Skip(count * (page - 1))
                .Take(count)
                .ToList();
            if (pageZoneList.Count == 0)
            {
                pageZoneList = null;
            }

            return pageZoneList;
        }

        public async Task<List<Zone>> GetAll(CancellationToken cancellationToken)
        {
            var zone = await _context.Zones.ToListAsync(cancellationToken);
            return zone;
        }

   /// Get Zone qua Country
        public async Task<ICollection<Zone>> GetZoneCountryId(int countryId, CancellationToken cancellationToken)
        {
            var zone = await _context.Zones.Where(x => x.CountryId == countryId).ToListAsync(cancellationToken);
            return zone;
        }
        public async Task<List<Zone>> PublicGetZoneAll(int countryId, CancellationToken cancellationToken)
        {
            var zone = await _context.Zones.Where(x => x.CountryId == countryId).ToListAsync(cancellationToken);
            zone = zone.Where(x => x.Active).ToList();
            return zone;
        }
        public async Task<ICollection<Zone>> GetZoneAllByCountry(int countryId, CancellationToken cancellationToken)
        {
            return await _context.Zones.Where(x => x.CountryId == countryId).ToListAsync(cancellationToken);
        }
        public async Task<ICollection<Zone>> PublicGetZonePage(int countryId, int page, int count, CancellationToken cancellationToken)
        {
            var zones = await _context.Zones.Where(x => x.CountryId == countryId).ToListAsync(cancellationToken);
            zones = zones.Where(x => x.Active).ToList();
            var pageZones = zones
                .Skip(count * (page - 1))
                .Take(count)
                .ToList();
            if (pageZones.Count == 0)
            {
                pageZones = null;
            }

            return pageZones;
        }
    }
}
