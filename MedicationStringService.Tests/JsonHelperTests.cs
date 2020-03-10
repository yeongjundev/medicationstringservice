using System.IO;
using System.Text;
using MedicationStringService.API.Helpers;
using Newtonsoft.Json.Linq;
using Xunit;

namespace MedicationStringService.Tests
{
    public class JsonHelperTests
    {
        [Fact]
        public async void JsonHelper_StreamParseToJObject_ReturnNotNull()
        {
            string strJson =
                @"{
                    'item1': 'test1'
                }";
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
    }
}