using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

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
			if (cl.SteamId == 76561198799754743)
			{
				base.DoPlayerDevCam(cl);
			}
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
		}

		[ServerCmd("spawn")]
		public static void Spawn(string modelname)
		{
			var owner = ConsoleSystem.Caller?.Pawn;

			if (ConsoleSystem.Caller == null)
				return;

			var tr = Trace.Ray(owner.EyePos, owner.EyePos + owner.EyeRot.Forward * 500)
				.UseHitboxes()
				.Ignore(owner)
				.Size(2)
				.Run();

			var ent = new Prop();
			ent.Position = tr.EndPos;
			ent.Rotation = Rotation.From(new Angles(0, owner.EyeRot.Angles().yaw, 0)) * Rotation.FromAxis(Vector3.Up, 180);
			ent.SetModel(modelname);

			// Drop to floor
			if (ent.PhysicsBody != null && ent.PhysicsGroup.BodyCount == 1)
			{
				var p = ent.PhysicsBody.FindClosestPoint(tr.EndPos);

				var delta = p - tr.EndPos;
				ent.PhysicsBody.Position -= delta;
				//DebugOverlay.Line( p, tr.EndPos, 10, false );
			}

		}

		[ServerCmd("spawn_entity")]
		public static void SpawnEntity(string entName)
		{
			var owner = ConsoleSystem.Caller.Pawn;

			if (owner == null)
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
			if (player.Pawn is Player basePlayer)
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
	}

}
