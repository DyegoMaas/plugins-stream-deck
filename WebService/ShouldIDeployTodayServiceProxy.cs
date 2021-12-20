using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace plugins_stream_deck.WebService;

public class ShouldIDeployTodayServiceProxy
{
    public static async Task<APIResponseModel> FindOut(string timezone)
    {
        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync($"https://shouldideploy.today/api?tz={timezone}");
        var json = await response.Content.ReadAsStringAsync();
        var responseModel = new JsonSerializer().Deserialize<APIResponseModel>(new JsonTextReader(new StringReader(json)));
        return responseModel;
    }
}