using Sandbox;

[Library]
public class CarController : PawnController
{
	public override void FrameSimulate()
	{
		base.FrameSimulate();

		Simulate();
	}

	public override void Simulate()
	{
		var player = Pawn as REnginePlayer;
		if ( !player.IsValid() ) return;

		var car = player.Vehicle as CarEntity;
		if ( !car.IsValid() ) return;

		car.Simulate( Client );

		if ( player.Vehicle == null )
		{
			Position = car.Position + car.Rotation.Up * (100 * car.Scale);
			Velocity += car.Rotation.Right * (200 * car.Scale);
			return;
		}

		EyeRot = Input.Rotation;
		EyePosLocal = Vector3.Up * (64 - 10) * car.Scale;
		Velocity = car.Velocity;

		SetTag( "noclip" );
		SetTag( "sitting" );
	}
}
