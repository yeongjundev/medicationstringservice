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

        // Get the total number of all MedicationStrings.
        public Task<int> TotalCount()
        {
            return context.CountAsync();
        }

        // Get the sum of DosageCounts for all MedicationStrings.
        public Task<int> TotalDosageCount()
        {
            return context.SumAsync(medicationString => medicationString.DosageCount);
        }

        // Get the number of MedicationStrings per each BottleSize.
        public Task<List<CountByBottleSize>> TotalNumberByBottleSize()
        {
            var result = context
                .GroupBy(ms => ms.BottleSize)
                .Select(g => new CountByBottleSize
                {
                    BottleSize = g.Key,
                    Count = g.Count()
                });
            return result.ToListAsync();
        }

        // Get the number of MedicationStrings per each MedicationId.
        public Task<List<CountByMedicationId>> DistinctMedicationIds()
        {
            var result = context
                .GroupBy(ms => ms.MedicationId)
                .Select(g => new CountByMedicationId
                {
                    MedicationId = g.Key,
                    Count = g.Count()
                });
            return result.ToListAsync();
        }
    }
}