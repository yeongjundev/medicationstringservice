using System;
using System.Collections.Generic;
using MedicationStringService.API.Models;
using Newtonsoft.Json.Linq;

namespace MedicationStringService.API.Services
{
    // Extract string of MedicationStrings from JToken and create
    // MedicationString object if it is valid.
    // MedicationString objects are created when Build() is called.
    public class MedicationStringBuilder
    {
        private JToken _token;

        private IEnumerable<string> _strMedicationStrings;

        public MedicationStringBuilder(JToken medicationStringsToken)
        {
            _token = medicationStringsToken;

            if (_token.Type.Equals(JTokenType.String))
            {
                string[] tmp = _token.Value<string>().Split(";");
                _strMedicationStrings = new List<string>(tmp);
            }
            else
            {
                _strMedicationStrings = _token.Values<string>();
            }
        }

        public IEnumerable<MedicationString> Build()
        {
            var medicationStrings = new List<MedicationString>();

            foreach (string strMedicationString in _strMedicationStrings)
            {
                var medicationString = _CreateFromStrMedicationString(strMedicationString);
                if (medicationString != null)
                {
                    medicationStrings.Add(medicationString);
                }
            }
            return medicationStrings;
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
    }
}