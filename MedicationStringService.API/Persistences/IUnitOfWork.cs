using System;
using System.Threading.Tasks;
using MedicationStringService.API.Repositories;

namespace MedicationStringService.API.Persistences
{
    public interface IUnitOfWork : IDisposable
    {
        IMedicationStringRepository MedicationStringRepo { get; }

        Task<int> Complete();
    }
}