using System;
using System.IO;
using System.Text;
using MedicationStringService.API.Helpers;
using Newtonsoft.Json.Linq;
using Xunit;

namespace MedicationStringService.Tests
{
    public class JsonHelperTests
    {
        private const string _medicationStringsJSchema =
            @"{
                'description': 'Post MedicationStrings',
                'type': 'object',
                'properties': {
                    'medicationStrings': {
                        'type': ['array', 'string'],
                        'items': {
                            'type': 'string'
                        }
                    }
                },
                'required': ['medicationStrings']
            }";

        [Fact]
        public async void JsonHelper_StreamParseToJObject_ReturnNotNull()
        {
            string strJson = @"{ 'item1': 'test1' }";
            byte[] byteStream = Encoding.UTF8.GetBytes(strJson);
            MemoryStream stream = new MemoryStream(byteStream);

            var json = await stream.ToJObject();

            Assert.NotNull(json);
        }

        [Fact]
        public async void JsonHelper_StreamParseToJObject_ReturnNull()
        {
            string strJson = @"non_json_format";
            byte[] byteStream = Encoding.UTF8.GetBytes(strJson);
            MemoryStream stream = new MemoryStream(byteStream);

            var json = await stream.ToJObject();

            Assert.Null(json);
        }

        [Fact]
        public void JsonHelper_JsonFormatValidation_ArrayOfString_ReturnTrue()
        {
            string strJson = @"{
                'medicationStrings': ['186FASc73541_M_1058', '18673cda541_S_0061', '18673541_S_0146']
            }";
            JObject json = JObject.Parse(strJson);

            bool result = json.Validate(_medicationStringsJSchema);

            Assert.True(result);
        }

        [Fact]
        public void JsonHelper_JsonFormatValidation_String_ReturnTrue()
        {
            string strJson = @"{
                'medicationStrings': '186FASc73541_M_1058;18673cda541_S_0061;18673541_S_0146'
            }";
            JObject json = JObject.Parse(strJson);

            bool result = json.Validate(_medicationStringsJSchema);

            Assert.True(result);
        }

        [Fact]
        public void JsonHelper_JsonFormatValidation_InvalidFormats_ReturnFalse()
        {
            string[] strJsons = {
                @"{ 'medicationStrings': 0000 }", // integer
                @"{ 'notMedicationStrings': '186FASc73541_M_1058' }" // no medicationString property
            };
            foreach (var strJson in strJsons)
            {
                JObject json = JObject.Parse(strJson);
                bool result = json.Validate(_medicationStringsJSchema);
                Assert.False(result);
            }
        }
    }
}