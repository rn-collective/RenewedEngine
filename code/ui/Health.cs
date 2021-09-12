using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public class Health : Panel
{
	public Label Label;

	public Health()
	{
		Label = Add.Label( "100", "value" );
	}

	public override void Tick()
	{
		var player = Local.Pawn;
		if ( player == null )
			return;
		SetClass("open", Input.Down(InputButton.Score));

		int health = player.Health.CeilToInt();
		string text = "undefined";

		if (health <= 75 && health > 50)
		{
			text = "Я не слишком здоров, но и не умру";
		}
		else if (health < 50 && health > 10)
		{
			text = "Я чувствую себя плохо";
		}
		else if (health < 10 && health > 0)
		{
			text = "Я скоро умру...";
		} else if (health <= 0)
		{
			text = "Я умер...";
		}
		else
		{
			text = "Я здоров";
		}

		Label.Text = $"{text}";
	}
}
