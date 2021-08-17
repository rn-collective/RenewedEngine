using Sandbox;
using System;
using System.Linq;

namespace REngine
{
	partial class REnginePlayer : Player
	{
		public override void Respawn()
		{
			SetModel( "models/citizen/citizen.vmdl" );

			Controller = new WalkController();
			Animator = new StandardPlayerAnimator();
			Camera = new FirstPersonCamera();

			EnableAllCollisions = true;
			EnableDrawing = true;
			EnableHideInFirstPerson = true;
			EnableShadowInFirstPerson = false;

			base.Respawn();
		}

		/// <summary>
		/// Called every tick, clientside and serverside.
		/// </summary>
		public override void Simulate( Client cl )
		{
			base.Simulate( cl );

			SimulateActiveChild( cl, ActiveChild );
		}

		public override void OnKilled()
		{
			base.OnKilled();
			Event.Run("OnPlayerKilled");
		}

		[Event("OnPlayerKilled")]
		public void OnPlayerKilled() {
			EnableDrawing = false;
		}
	}
}
