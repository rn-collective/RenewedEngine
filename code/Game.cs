
using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.IO;
using System.Threading.Tasks;

//
// You don't need to put things in a namespace, but it doesn't hurt.
//
namespace REngine
{

	/// <summary>
	/// This is your game class. This is an entity that is created serverside when
	/// the game starts, and is replicated to the client. 
	/// 
	/// You can use this to create things like HUDs and declare which player class
	/// to use for spawned players.
	/// </summary>
	[Library( "rengine", Title = "Renewed Engine" )]
	public partial class REngine : Sandbox.Game
	{
		public REngine()
		{
			if ( IsServer )
			{
				Log.Info( "My Gamemode Has Created Serverside!" );

				// Create a HUD entity. This entity is globally networked
				// and when it is created clientside it creates the actual
				// UI panels. You don't have to create your HUD via an entity,
				// this just feels like a nice neat way to do it.
				new REngineHUD();
			}

			if ( IsClient )
			{
				Log.Info( "My Gamemode Has Created Clientside!" );
			}
		}

		/// <summary>
		/// A client has joined the server. Make them a pawn to play with
		/// </summary>
		public override void ClientJoined( Client client )
		{
			base.ClientJoined( client );

			var player = new REnginePlayer();
			client.Pawn = player;

			player.Respawn();
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
		}
	}
}
