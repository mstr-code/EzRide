using System.Net.Http;
using System.Text;

using System.Threading.Tasks;
using EzRide.Api;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;

namespace EzRide.Tests.EndToEnd.Controllers
{
    public abstract class ControllerTestsBase
    {
        protected TestServer Server { get; }
        protected HttpClient Client { get; }

        protected ControllerTestsBase()
        {
            Server = new TestServer(WebHost.CreateDefaultBuilder().UseStartup<Startup>());
            Client = Server.CreateClient();
        }

        protected static StringContent GetSerializedPayload(object data)
        {
            // Serialize object 'data' to string.
            string json = JsonConvert.SerializeObject(data);

            // Return StringContent using UTF-8 encoding,
            // with Content-Type: "application/json"
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        protected static async Task<T> GetDeserializedPayload<T>(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            
            // Fetch the content from the response and save its string representation.
            string resposeString = await response.Content.ReadAsStringAsync();
            
            // Return deserialized object from its string representation.
            return JsonConvert.DeserializeObject<T>(resposeString);
        }
    }   
}