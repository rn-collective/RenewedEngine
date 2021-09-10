
using Sandbox;
using Sandbox.Hooks;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using System.Collections.Generic;

namespace Sandbox.UI
{
	public partial class Scoreboard<T> : Panel where T : ScoreboardEntry, new()
	{
		public Panel Canvas { get; protected set; }
		Dictionary<int, T> Entries = new ();

		public Panel Header { get; protected set; }

		public Panel Button;
		public bool State = false;

		public Scoreboard()
		{
			StyleSheet.Load( "/ui/Scoreboard/Scoreboard.scss" );
			AddClass( "scoreboard" );

			AddHeader();

			Canvas = Add.Panel( "canvas" );

			PlayerScore.OnPlayerAdded += AddPlayer;
			PlayerScore.OnPlayerUpdated += UpdatePlayer;
			PlayerScore.OnPlayerRemoved += RemovePlayer;

			foreach ( var player in PlayerScore.All )
			{
				AddPlayer( player );
			}

			Scoreboard_Toggle.OnOpenScoreboard += Open;
		}
		public void Open()
		{
			if ( State == false )
				AddClass( "open" );
			else if ( State == true )
				RemoveClass( "open" );
			State = !State;
		}

		public override void Tick()
		{
			base.Tick();
		}

		protected virtual void AddHeader() 
		{
			Header = Add.Panel( "header" );
			Header.Add.Label( "Name", "name" );
			Header.Add.Label( "Id", "id" );
			Header.Add.Label( "Kills", "kills" );
			Header.Add.Label( "Deaths", "deaths" );
			Header.Add.Label( "Ping", "ping" );
		}

		protected virtual void AddPlayer( PlayerScore.Entry entry )
		{
			var p = Canvas.AddChild<T>();
			p.UpdateFrom( entry );

			Entries[entry.Id] = p;
		}

		protected virtual void UpdatePlayer( PlayerScore.Entry entry )
		{
			if ( Entries.TryGetValue( entry.Id, out var panel ) )
			{
				panel.UpdateFrom( entry );
			}
		}

		protected virtual void RemovePlayer( PlayerScore.Entry entry )
		{
			if ( Entries.TryGetValue( entry.Id, out var panel ) )
			{
				panel.Delete();
				Entries.Remove( entry.Id );
			}
		}
	}
}


namespace Sandbox.Hooks
{
	public static partial class Scoreboard_Toggle
	{
		public static event Action OnOpenScoreboard;

		[ClientCmd( "admin_scoreboard" )]
		internal static void MessageMode()
		{
			OnOpenScoreboard?.Invoke();
		}

	}
}
