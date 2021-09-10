using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public class Hunger : Panel
{
	public Label Label;

	public Hunger()
	{
		Label = Add.Label("100", "value");
	}

	public override void Tick()
	{
		var player = Local.Pawn;
		if (player == null)
			return;
		int health = player.Health.CeilToInt();
		string text = "undefined";
		int colorType = 0;

		if (health < 75 && health > 50)
		{
			text = "Я чувствую себя не здоровым";
			colorType = 1;
		}
		else if (health < 50 && health > 10)
		{
			text = "Я чувствую как я умираю";
			colorType = 1;
		}
		else if (health < 10)
		{
			text = "Мне очень плохо";
			colorType = 2;
		}
		else
		{
			text = "Я здоров";
			colorType = 0;
		}

		if (colorType == 0)
		{
			Label.SetClass("green", true);
		}
		else if (colorType == 1)
		{
			Label.SetClass("orange", true);
		}
		else if (colorType == 2)
		{
			Label.SetClass("red", true);
		}

		Label.Text = $"{text}";
	}
}
