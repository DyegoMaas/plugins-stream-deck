namespace plugins_stream_deck.Models
{
  public class ShouldIDeployTodaySettingsModel
  {
	public string TimeZone { get; set; } = "UTC";
    public bool ShouldIDeployToday { get; set; }
  }
}
