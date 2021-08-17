
using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.IO;
using System.Threading.Tasks;

namespace REngine
{
	[Library("rengine", Title = "Renewed Engine")]

	public partial class REngine : Sandbox.Game
	{
		public static REngine Current { get; protected set; }
		public REngine()
		{
			if (IsServer)
			{
				_ = new REngineHUD();
			}
			Current = this;
		}

		public bool isDeveloperMode = true;
		public async void Debug(params object[] args)
		{
			if (isDeveloperMode)
			{
				foreach (object arg in args)
				{
					Log.Warning("RENGINE DEBUG => " + arg);
				}
			}
		}

		}

		public override void ClientJoined(Client cl)
		{
			base.ClientJoined(cl);

			var player = new REnginePlayer();
			cl.Pawn = player;

			player.Respawn();
			player.Tags.Add("initialized");
			Event.Run("OnClientInitialized", cl);
		}

		public override void DoPlayerNoclip(Client cl) {
			if (cl.Pawn.Tags.Has("isAdmin") && cl.Pawn is Player basePlayer)
			{
				base.DoPlayerNoclip(cl);
				Event.Run("PostPlayerNoclipped", cl);
			}
		}

		public override void PostLevelLoaded()
		{
			base.PostLevelLoaded();
			Event.Run("PostLevelLoaded");
		}

		public override void DoPlayerSuicide(Client cl) { }

		[Event("OnClientInitialized")]
		public async void OnClientInitialized(Client cl)
		{
			IsBanned(cl);
			IsAdmin(cl);
		}

		private void IsAdmin(Client cl)
		{
			if (cl.SteamId == 76561198799754743)
				cl.Pawn.Tags.Add("isAdmin");
		}
		
		private void IsBanned(Client cl)
		{
			if (cl.SteamId == 342) { } // TO DO
		}

		[Event("PostLevelLoaded")]
		public void Callback()
		{
			if (IsClient) { }
		}
	}
}
