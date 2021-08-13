using Sandbox.UI;

//
// You don't need to put things in a namespace, but it doesn't hurt.
//
namespace REngine
{
	/// <summary>
	/// This is the HUD entity. It creates a RootPanel clientside, which can be accessed
	/// via RootPanel on this entity, or Local.Hud.
	/// </summary>
	public partial class REngineHUD : Sandbox.HudEntity<RootPanel>
	{
		public REngineHUD()
		{
			if ( IsClient )
			{
				RootPanel.SetTemplate( "/Hud.html" );
			}
		}
	}

}
