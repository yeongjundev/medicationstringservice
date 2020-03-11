using System;
using System.Collections.Generic;
using MedicationStringService.API.Models;
using MedicationStringService.API.Persistences;
using Newtonsoft.Json.Linq;

namespace MedicationStringService.API.Services
{
    // Extract string of MedicationStrings from JToken and create
    // MedicationString object if it is valid.
    // MedicationString objects are created when Build() is called.
    public class MedicationStringBuilder : IMedicationStringBuilder
    {
        private readonly IUnitOfWork _uow;

        public MedicationStringBuilder(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IEnumerable<MedicationString> Build(JToken medicationStringsToken)
        {
            var strMedicationStrings = _GetStrMedicationStrings(medicationStringsToken);
            var medicationStrings = new List<MedicationString>();

            foreach (string strMedicationString in strMedicationStrings)
            {
                var medicationString = _CreateFromStrMedicationString(strMedicationString);
                if (medicationString != null)
                {
                    medicationStrings.Add(medicationString);
                }
            }
            return medicationStrings;
        }

        private IEnumerable<string> _GetStrMedicationStrings(JToken token)
        {
            if (token.Type.Equals(JTokenType.String))
            {
                string[] tmp = token.Value<string>().Split(";");
                return new List<string>(tmp);
            }
            else
            {
                return token.Values<string>();
            }
        }

        private MedicationString _CreateFromStrMedicationString(string strMedicationString)
        {
            string[] tmp = strMedicationString.Split("_");
            if (tmp.Length != 3)
            {
                return null;
            }

            // Validate medicationId
            string medicationId = tmp[0];
            if (medicationId.Length < 1 || medicationId.Length > 20)
            {
                return null;
            }

            // Validate bottleSize
            BottleSizeEnum bottleSize;
            if (!Enum.TryParse<BottleSizeEnum>(tmp[1], out bottleSize))
            {
                return null;
            }

            // Validate dosageCount
            if (tmp[2].Length != 4)
            {
                return null;
            }
            int dosageCount;
            if (!int.TryParse(tmp[2], out dosageCount))
            {
                return null;
            }

            return new MedicationString()
            {
                MedicationId = medicationId,
                BottleSize = bottleSize,
                DosageCount = dosageCount
            };
        }

        public void AddMedicationStrings(IEnumerable<MedicationString> medicationStrings)
        {
            _uow.MedicationStringRepo.AddRange(medicationStrings);
        }
    }
}