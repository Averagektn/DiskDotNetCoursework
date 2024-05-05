using Disk.Db.Context;
using Disk.Entity;
using Disk.Repository.Interface;

namespace Disk.Repository.Implemetation
{
    public class AddressRepository : IAddressRepository
    {
        private readonly DiskContext _context = new();

        public async Task<List<Region>> GetRegionsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<District>> GetDistrictsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Address> AddAddressIfNotExistsAsync(Address address)
        {
            throw new NotImplementedException();
        }

        public async Task<Region> AddRegion(Region region)
        {
            var res = await _context.Regions.AddAsync(region);
            await _context.SaveChangesAsync();

            return res.Entity;
        }

        public async Task<District> AddDistrict(District district)
        {
            throw new NotImplementedException();
        }
    }
}
