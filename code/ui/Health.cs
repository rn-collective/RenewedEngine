﻿using Sandbox;
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
		int colorType = 0;

		if (health < 75 && health > 50)
		{
			text = "Я не слишком здоров, но и не умру";
			colorType = 1;
		}
		else if (health < 50 && health > 10)
		{
			text = "Я чувствую себя очень плохо";
			colorType = 1;
		}
		else if (health < 10 && health > 0)
		{
			text = "Я скоро умру...";
			colorType = 2;
		} else if (health <= 0)
		{
			text = "Я умер...";
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
		} else if (colorType == 1)
		{
			Label.SetClass("orange", true);
		} else if (colorType == 2)
		{
			Label.SetClass("red", true);
		}

		Label.Text = $"{text}";
	}
}
