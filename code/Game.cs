using Sandbox;

namespace REngine
{
	[Library("rengine", Title = "Renewed Engine")]
	public partial class REngine : Sandbox.Game
	{
		public REngineHud REngineHudEnt;

		public REngine()
		{
			if (IsClient)
			{
				REngineHudEnt = new REngineHud();
			}
		}

		[Event("Event.Hotloaded")]
		public void Hotloaded()
		{
			if (IsClient)
			{
				REngineHudEnt?.Delete();
				REngineHudEnt = new REngineHud();
				Log.Info("REngineHudEnt removed and instanced one");
			}
		}

		public override void ClientJoined(Client cl)
		{
			base.ClientJoined(cl);

			var player = new REnginePlayer();
			cl.Pawn = player;

			Event.Run("PrePlayerConnected", cl);
			player.Respawn();
			Event.Run("PostPlayerConnected", cl);
		}

		[Event("PrePlayerConnected")]
		public async void PrePlayerConnected(Client cl)
		{
			IsAdmin(cl);
		}

		private void IsAdmin(Client cl)
		{
			if (cl.SteamId == 76561198799754743)
			{
				cl.Pawn.Tags.Add("isAdmin");
			}
		}

		public override void DoPlayerDevCam(Client cl)
		{
			if (cl.Pawn.Tags.Has("isAdmin"))
			{
				base.DoPlayerDevCam(cl);
			}
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
		}

		public override void DoPlayerSuicide(Client _) { }

		[ServerCmd("spawn")]
		public static void Spawn(string modelname)
		{
			var owner = ConsoleSystem.Caller?.Pawn;

			if (ConsoleSystem.Caller == null)
				return;

			if (!owner.Tags.Has("isAdmin"))
				return;

			var tr = Trace.Ray(owner.EyePos, owner.EyePos + owner.EyeRot.Forward * 500)
				.UseHitboxes()
				.Ignore(owner)
				.Run();

			var ent = new Prop();
			ent.Position = tr.EndPos;
			ent.Rotation = Rotation.From(new Angles(0, owner.EyeRot.Angles().yaw, 0)) * Rotation.FromAxis(Vector3.Up, 180);
			ent.SetModel(modelname);
			ent.Position = tr.EndPos - Vector3.Up * ent.CollisionBounds.Mins.z;
		}

		[ServerCmd("spawn_entity")]
		public static void SpawnEntity(string entName)
		{
			var owner = ConsoleSystem.Caller.Pawn;

			if (owner == null)
				return;

			if (!owner.Tags.Has("isAdmin"))
				return;

			var attribute = Library.GetAttribute(entName);

			if (attribute == null || !attribute.Spawnable)
				return;

			var tr = Trace.Ray(owner.EyePos, owner.EyePos + owner.EyeRot.Forward * 200)
				.UseHitboxes()
				.Ignore(owner)
				.Size(2)
				.Run();

			var ent = Library.Create<Entity>(entName);
			if (ent is BaseCarriable && owner.Inventory != null)
			{
				if (owner.Inventory.Add(ent, true))
					return;
			}

			ent.Position = tr.EndPos;
			ent.Rotation = Rotation.From(new Angles(0, owner.EyeRot.Angles().yaw, 0));
		}

		public override void DoPlayerNoclip(Client player)
		{
			if (player.Pawn is Player basePlayer && player.Pawn.Tags.Has("isAdmin"))
			{
				if (basePlayer.DevController is NoclipController)
				{
					basePlayer.DevController = null;
				}
				else
				{
					basePlayer.DevController = new NoclipController();
				}
			}
		}

		[ClientCmd("debug_write")]
		public static void Write()
		{
			ConsoleSystem.Run("quit");
		}
	}
}
