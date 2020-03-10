using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicationStringService.API.DTOs;
using MedicationStringService.API.Models;
using MedicationStringService.API.Persistences;
using Microsoft.EntityFrameworkCore;

namespace MedicationStringService.API.Repositories
{
    public class MedicationStringRepository : Repository<MedicationString>, IMedicationStringRepository
    {
        public MedicationStringRepository(DbSet<MedicationString> dbSet) : base(dbSet) { }

        public async Task<int> TotalCount()
        {
            return await context.CountAsync();
        }

        public async Task<int> TotalDosageCount()
        {
            return await context.SumAsync(medicationString => medicationString.DosageCount);
        }

        public async Task<IEnumerable<CountByBottleSize>> TotalNumberByBottleSize()
        {
            IEnumerable<CountByBottleSize> result = await context
                .GroupBy(ms => ms.BottleSize)
                .Select(g => new CountByBottleSize
                {
                    BottleSize = g.Key,
                    Count = g.Count()
                }).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<CountByMedicationId>> DistinctMedicationIds()
        {
            IEnumerable<CountByMedicationId> result = await context
                .GroupBy(ms => ms.MedicationId)
                .Select(g => new CountByMedicationId
                {
                    MedicationId = g.Key,
                    Count = g.Count()
                }).ToListAsync();
            return result;
        }
    }
}