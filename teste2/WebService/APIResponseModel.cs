using System.Text.Json.Serialization;

namespace plugins_stream_deck.WebService;

public class APIResponseModel
{
    [JsonPropertyName("timezone")]
    public string TimeZone { get; set; }
		
    [JsonPropertyName("shouldideploy")]
    public bool ShouldIDeploy { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }
}