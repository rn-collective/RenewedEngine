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
				text = "Пуст";
			}
			else if (weapon.AmmoClip >= weapon.ClipSize)
			{
				text = "Полон";
			}
			else if (weapon.AmmoClip > weapon.ClipSize / 2)
			{
				text = "Почти полон";
			}
			else if (weapon.AmmoClip == weapon.ClipSize / 2)
			{
				text = "1~2";
			}
			else if (weapon.AmmoClip < weapon.ClipSize / 2)
			{
				text = "Почти пуст";
			}
			Weapon.Text = text;
		}
	}
}
