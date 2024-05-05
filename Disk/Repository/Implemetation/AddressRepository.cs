using Disk.Db.Context;
using Disk.Entity;
using Disk.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Disk.Repository.Implemetation
{
    public class AddressRepository : IAddressRepository
    {
        private readonly DiskContext _context = new();

        public async Task<List<Region>> GetRegionsAsync()
        {
            return await _context.Regions.ToListAsync();
        }

        public async Task<List<District>> GetDistrictsAsync()
        {
            return await _context.Districts.ToListAsync();
        }

        public async Task<Address> AddAddressIfNotExistsAsync(Address address)
        {
            var exists = await _context.Addresses.Where(a => a.Corpus == address.Corpus && a.District == address.District 
                    && a.Apartment == address.Apartment && a.House == address.House && a.Street == address.Street)
                .FirstOrDefaultAsync();

            if (exists is null)
            {
                var res = await _context.Addresses.AddAsync(address);
                await _context.SaveChangesAsync();
                return res.Entity;
            }

            return exists;
        }

        public async Task<Region> AddRegion(Region region)
        {
            var res = await _context.Regions.AddAsync(region);
            await _context.SaveChangesAsync();

            return res.Entity;
        }

        public async Task<District> AddDistrict(District district)
        {
            var res = await _context.Districts.AddAsync(district);
            await _context.SaveChangesAsync();

            return res.Entity;
        }
    }
}
