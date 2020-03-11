using System.Collections.Generic;
using MedicationStringService.API.Models;
using Newtonsoft.Json.Linq;

namespace MedicationStringService.API.Services
{
    public interface IMedicationStringBuilder
    {
        IEnumerable<MedicationString> Build(JToken medicationStringsToken);

        void AddMedicationStrings(IEnumerable<MedicationString> medicationStrings);
    }
}