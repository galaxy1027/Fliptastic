using Godot;

public partial class player : CharacterBody2D
{
	[Export] private float speed = 200.0f;
	[Export] private float jumpVelocity = -150.0f;
	[Export] private float doubleJumpVelocity = -100.0f;


	// Get the gravity from the project settings to be synced with RigidBody nodes.
	private float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	private bool gravityFlipped = false;
	private bool hasDoubleJumped = false;
	private Vector2 direction;
	private Vector2 _startingPosition;
	
	
	public override void _Ready()
	{
		_startingPosition = GlobalPosition;
		GetNode<AudioStreamPlayer>("JumpSound").Stream = GD.Load<AudioStream>("res://assets/audio/boing.mp3");

	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity, update double jump status.
		if (!IsOnFloor() || !IsOnCeiling())
			velocity.Y += gravity * (float)delta;
		if (IsOnFloor() || IsOnCeiling())
			hasDoubleJumped = false;

		// Handle Jump.
		if (Input.IsActionJustPressed("jump"))
		{
			if (IsOnFloor())
			{
				velocity.Y = jumpVelocity;
				GetNode<AudioStreamPlayer>("JumpSound").Play();
			}
			else if (IsOnCeiling())
			{
				velocity.Y = -jumpVelocity;
				GetNode<AudioStreamPlayer>("JumpSound").Play();
			}
			else if (!hasDoubleJumped)
			{
				if (!gravityFlipped)
					velocity.Y += doubleJumpVelocity;
				else
					velocity.Y += -doubleJumpVelocity;
				hasDoubleJumped = true;
				GetNode<AudioStreamPlayer>("JumpSound").Play();
			}
			
		}

		//Handle gravity flip
		if (Input.IsActionJustPressed("gravity"))
		{
			gravity *= -1;
			gravityFlipped = !gravityFlipped;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		direction = Input.GetVector("left", "right", "up", "down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, speed);
		}

		Velocity = velocity;
		MoveAndSlide();
		UpdateAnimation();
		UpdateDirection();
	}

	private void UpdateAnimation()
	{
		if (direction != Vector2.Zero)
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("run");
		else
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").Stop();
	}

	private void UpdateDirection()
	{
		if (direction.X > 0)
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").FlipH = true;
		if (direction.X < 0)
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").FlipH = false;
		if (gravityFlipped)
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").FlipV = true;
		else
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").FlipV = false;
	}
	private void _on_hazard_detector_area_entered(Area2D area)
	{
		GlobalPosition = _startingPosition;
	}
}






