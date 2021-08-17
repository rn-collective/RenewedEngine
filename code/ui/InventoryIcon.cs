
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public class InventoryIcon : Panel
{
	public Entity TargetEnt;
	public Label Label;

	public InventoryIcon( int i, Panel parent )
	{
		Parent = parent;
		Label = Add.Label( "empty", "item-name" );
	}

	public void Clear()
	{
		Label.Text = "";
		SetClass( "active", false );
	}
}
