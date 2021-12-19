using StreamDeckLib;
using StreamDeckLib.Messages;
using System;
using System.Threading.Tasks;
using plugins_stream_deck.Models;
using plugins_stream_deck.WebService;

namespace plugins_stream_deck
{
	[ActionUuid(Uuid="br.com.dyegomaas.developerwidson.shouldideploytoday.DefaultPluginAction")]
	public class ShouldIDeployTodayAction : BaseStreamDeckActionWithSettingsModel<Models.ShouldIDeployTodaySettingsModel>
	{
		private const string UndecidedMessage = "Should\nI deploy\ntoday?";

		public override async Task OnKeyUp(StreamDeckEventPayload args)
		{
			var response = await ShouldIDeployTodayServiceProxy.FindOut(SettingsModel.TimeZone);
			SettingsModel.ShouldIDeployToday = response.ShouldIDeploy;

			await Manager.SetTitleAsync(args.context, GetRecommendationText());

			//update settings
			await Manager.SetSettingsAsync(args.context, SettingsModel);

			await Task.Delay(TimeSpan.FromSeconds(10));
			ResetState(args);
		}

		private async Task ResetState(StreamDeckEventPayload args)
		{
			SettingsModel.ShouldIDeployToday = false;
			await Manager.SetSettingsAsync(args.context, SettingsModel);
			
			await Manager.SetTitleAsync(args.context, UndecidedMessage);
		}

		public override async Task OnDidReceiveSettings(StreamDeckEventPayload args)
		{
			await base.OnDidReceiveSettings(args);

			await Manager.SetTitleAsync(args.context, GetRecommendationText());
		}
		
		private string GetRecommendationText()
		{
			return SettingsModel.ShouldIDeployToday ? "Yes" : "No!";
		}

		public override async Task OnWillAppear(StreamDeckEventPayload args)
		{
			await base.OnWillAppear(args);
			await Manager.SetTitleAsync(args.context, UndecidedMessage);
		}
	}
}
