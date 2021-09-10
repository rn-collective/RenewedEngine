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
		SetClass("open", Input.Down(InputButton.Score));

		if (weapon == null || weapon.HideFromAmmoHUD)
		{
			Weapon.Text = "";
			Inventory.Text = "";
		} else
		{
			string text = $"В этом оружии осталось ~{weapon.AmmoClip} патрон";

			if (weapon.AmmoClip <= 0)
			{
				text = "В оружии патроны отсутствуют...";
			}
			else if (weapon.AmmoClip >= weapon.ClipSize)
			{
				text = "В оружии полный магазин";
			}
			else if (weapon.AmmoClip > weapon.ClipSize / 2)
			{
				text = "В оружии патронов больше половины магазина";
			}
			else if (weapon.AmmoClip == weapon.ClipSize / 2)
			{
				text = "В оружии примерно осталась половина от всего магазина";
			}
			else if (weapon.AmmoClip < weapon.ClipSize / 3)
			{
				text = "В магазине оружия патронов почти не осталось";
			}
			else if (weapon.AmmoClip < weapon.ClipSize / 2)
			{
				text = "В оружии патронов осталось меньше половины";
			}
			Weapon.Text = text;
		}
	}
}
