namespace plugins_stream_deck.Models
{
  public class ShouldIDeployTodaySettingsModel
  {
	public string TimeZone { get; set; } = "Brazil/West";
    public bool ShouldIDeployToday { get; set; } = false;
  }
}
