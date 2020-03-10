using System.Threading.Tasks;
using MedicationStringService.API.Models;
using MedicationStringService.API.Repositories;
using Microsoft.Extensions.Configuration;

namespace MedicationStringService.API.Persistences
{
    // UnitOfWork pattern.
    // Currently application has only one MedicationStringRepository,
    // But, UnitOfWork pattern is adopted to improve extendability.
    // All future repositories will be also accessable via UnitOfWork.
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IConfiguration _config;

        private readonly AppDbContext _context;

        public UnitOfWork(IConfiguration config, AppDbContext context)
        {
            _config = config;
            _context = context;
        }

        private IMedicationStringRepository _medicationStringRepo;

        public IMedicationStringRepository MedicationStringRepo
        {
            get
            {
                if (_medicationStringRepo == null)
                {
                    _medicationStringRepo = new MedicationStringRepository(_context.Set<MedicationString>());
                }
                return _medicationStringRepo;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Task<int> Complete()
        {
            return _context.SaveChangesAsync();
        }
    }
}