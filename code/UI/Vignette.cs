using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public class Vignette : Panel
{
	public override void Tick()
	{
		var player = Local.Pawn;
		if (player == null)
			return;
		SetClass("open", Input.Down(InputButton.Score));
	}
}
