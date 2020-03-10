using System.Collections.Generic;
using MedicationStringService.API.Models;
using MedicationStringService.API.Services;
using Newtonsoft.Json.Linq;
using Xunit;

namespace MedicationStringService.Tests
{
    public class MedicationStringBuilderTests
    {
        [Fact]
        public void MedicationStringBuilder_Build_ReturnListOfMedicationStrings()
        {
            string strJson = @"{ 'medicationStrings': '186FASc73541_M_1058;18673cda541_S_0061;18673541_S_0146;not_valid_input;not_valid;' }";
            JObject json = JObject.Parse(strJson);

            var builder = new MedicationStringBuilder(json.GetValue("medicationStrings"));
            var medicationStrings = (List<MedicationString>)(builder.Build());

            var expected = new List<MedicationString>()
            {
                new MedicationString {
                    MedicationId = "186FASc73541",
                    BottleSize = BottleSizeEnum.M,
                    DosageCount = 1058
                },
                new MedicationString {
                    MedicationId = "18673cda541",
                    BottleSize = BottleSizeEnum.S,
                    DosageCount = 61
                },
                new MedicationString {
                    MedicationId = "18673541",
                    BottleSize = BottleSizeEnum.S,
                    DosageCount = 146
                },
            };

            Assert.True(expected.Count == medicationStrings.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.True(expected[i].MedicationId == medicationStrings[i].MedicationId);
                Assert.True(expected[i].BottleSize == medicationStrings[i].BottleSize);
                Assert.True(expected[i].DosageCount == medicationStrings[i].DosageCount);
            }
        }
    }
}