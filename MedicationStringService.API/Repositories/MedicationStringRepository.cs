using MedicationStringService.API.Models;
using MedicationStringService.API.Persistences;
using Microsoft.EntityFrameworkCore;

namespace MedicationStringService.API.Repositories
{
    public class MedicationStringRepository : Repository<MedicationString>, IMedicationStringRepository
    {
        public MedicationStringRepository(DbSet<MedicationString> dbSet) : base(dbSet) { }
    }
}