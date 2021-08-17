using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public class Ammo : Panel
{
	public Label Weapon;
	public Label Inventory;

	public Ammo()
	{
		Weapon = Add.Label("100", "weapon");
		Inventory = Add.Label("100", "inventory");
	}

	public override void Tick()
	{
		var player = Local.Pawn;
		if (player == null) return;

		var weapon = player.ActiveChild as BaseRWeapon;
		SetClass("active", weapon != null);

		if (weapon == null)
		{
			Weapon.Text = "";
			Inventory.Text = "";
		} else
		{
			Weapon.Text = $"{weapon.AmmoClip}";

			var inv = weapon.AvailableAmmo();
			Inventory.Text = $" / {inv}";
			Inventory.SetClass("active", inv >= 0);

			if (weapon.AmmoClip == 0 && inv == 0)
			{
				Weapon.Text = "";
				Inventory.Text = "";
				Inventory.SetClass("active", inv >= 0);
			}
		}
	}
}
