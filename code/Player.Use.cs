using Sandbox;

partial class REnginePlayer
{
	public bool IsUseDisabled()
	{
		return ActiveChild is IUse use && use.IsUsable( this );
	}

	protected override Entity FindUsable()
	{
		if ( IsUseDisabled() )
			return null;

		var tr = Trace.Ray( EyePos, EyePos + EyeRot.Forward * (85 * Scale) )
			.Radius( 2 )
			.HitLayer( CollisionLayer.Debris )
			.Ignore( this )
			.Run();

		if ( tr.Entity == null ) return null;
		if ( tr.Entity is not IUse use ) return null;
		if ( !use.IsUsable( this ) ) return null;

		return tr.Entity;
	}

	protected override void UseFail()
	{
		if ( IsUseDisabled() )
			return;

		base.UseFail();
	}
}
