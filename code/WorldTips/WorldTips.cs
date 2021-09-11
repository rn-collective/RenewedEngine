using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public class WorldTips : WorldPanel
{
	public WorldTips()
	{
		PanelBounds = new Rect(-360, -220, 720, 440);

		StyleSheet.Load("/WorldTips/WorldTips.scss");

		Add.Label("Hole 1", "hole");
		Add.Label("Coooool", "name");
		Add.Label("Par 3", "par");
	}
}
