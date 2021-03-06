using System.Collections.Generic;
using System.Threading.Tasks;
using MedicationStringService.API.DTOs;
using MedicationStringService.API.Models;
using MedicationStringService.API.Persistences;

namespace MedicationStringService.API.Repositories
{
    public interface IMedicationStringRepository : IRepository<MedicationString>
    {
        Task<int> TotalCount();

        Task<int> TotalDosageCount();

        Task<List<CountByBottleSize>> TotalNumberByBottleSize();

        Task<List<CountByMedicationId>> DistinctMedicationIds();
    }
}