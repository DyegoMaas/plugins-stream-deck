using StreamDeckLib;
using StreamDeckLib.Messages;
using System;
using System.Threading.Tasks;
using plugins_stream_deck.WebService;

namespace plugins_stream_deck
{
	public static class States
	{
		public const int Safe = 0;
		public const int Unsafe = 1;
	}
	
	[ActionUuid(Uuid="br.com.dyegomaas.developerwidson.shouldideploytoday.DefaultPluginAction")]
	public class ShouldIDeployTodayAction : BaseStreamDeckActionWithSettingsModel<Models.ShouldIDeployTodaySettingsModel>
	{
		private const string UndecidedMessage = "Should\nI deploy\ntoday?";

		public override async Task OnKeyDown(StreamDeckEventPayload args)
		{
			await base.OnKeyDown(args);

			await Manager.SetStateAsync(args.context, States.Safe);
			await Manager.SetTitleAsync(args.context, "...");
		}
		
		public override async Task OnKeyUp(StreamDeckEventPayload args)
		{
			var response = await ShouldIDeployTodayServiceProxy.FindOut(SettingsModel.TimeZone);
			await Task.Delay(TimeSpan.FromSeconds(3));

			SettingsModel.ShouldIDeployToday = response.ShouldIDeploy;
			await Manager.SetSettingsAsync(args.context, SettingsModel);
			
			await Manager.SetTitleAsync(args.context, GetRecommendationText());

			var newState = response.ShouldIDeploy ? States.Safe : States.Unsafe;
			await Manager.SetStateAsync(args.context, newState);

			await Task.Delay(TimeSpan.FromSeconds(10));
			await ResetState(args);
		}

		private async Task ResetState(StreamDeckEventPayload args)
		{
			await Manager.SetStateAsync(args.context, States.Safe);
			
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
			await Manager.SetStateAsync(args.context, States.Safe);
			await Manager.SetTitleAsync(args.context, UndecidedMessage);
		}
	}
}
