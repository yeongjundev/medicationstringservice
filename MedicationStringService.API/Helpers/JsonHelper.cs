using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MedicationStringService.API.Helpers
{
    public static class JsonHelper
    {
        public static async Task<JObject> ToJObject(this Stream stream)
        {
            try
            {
                using (var streamReader = new HttpRequestStreamReader(stream, Encoding.UTF8))
                {
                    using (var jsonReader = new JsonTextReader(streamReader))
                    {
                        return await JObject.LoadAsync(jsonReader);
                    }
                }
            }
            catch
            {
                return null;
            }
        }
    }
}