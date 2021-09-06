using Sandbox;

partial class REnginePlayer
{
	public bool IsUseDisabled()
	{
		return ActiveChild is IUse use && use.IsUsable(this);
	}

	protected override Entity FindUsable()
	{
		if (IsUseDisabled())
			return null;

		// First try a direct 0 width line
		var tr = Trace.Ray(EyePos, EyePos + EyeRot.Forward * (85 * Scale))
			.HitLayer(CollisionLayer.Debris)
			.Ignore(this)
			.Run();

		// Nothing found, try a wider search
		if (!IsValidUseEntity(tr.Entity))
		{
			tr = Trace.Ray(EyePos, EyePos + EyeRot.Forward * (85 * Scale))
			.Radius(2)
			.HitLayer(CollisionLayer.Debris)
			.Ignore(this)
			.Run();
		}

		// Still no good? Bail.
		if (!IsValidUseEntity(tr.Entity)) return null;

		return tr.Entity;
	}

	protected override void UseFail()
	{
		if (IsUseDisabled())
			return;

		base.UseFail();
	}
}
