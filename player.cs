using Godot;
using System;

public class player : KinematicBody2D
{
	Vector2 UP = new Vector2(0, -1);
	const int GRAVITY = 100;
	const int MAXFALLSPEED = 800;
	const int MAXSPEED = 800;
	const int JUMPFORCE = 1500;
	const int REBOUND = 2000;

	const int ACCEL = 100;
	Vector2 vZero = new Vector2();

	bool facing_right = true;
	bool can_jump = false;
	bool wjleft = true;
	bool wjright = true;

	Vector2 motion = new Vector2();

	Sprite currentSprite;
	AnimationPlayer animPlayer;
	RayCast2D leftLooker;
	RayCast2D rightLooker;
		// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		currentSprite = GetNode<Sprite>("sprite");
		animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		leftLooker = GetNode<RayCast2D>("lookLeft");
		rightLooker = GetNode<RayCast2D>("lookRight");
	}

	public override void _PhysicsProcess(float delta)
	{
		if(leftLooker.IsColliding() || rightLooker.IsColliding())
		{
			GD.Print($"detected");
		}
		motion.y += GRAVITY;
		
		if(motion.y > MAXFALLSPEED) {
			motion.y = MAXFALLSPEED;
		}

		if (facing_right) {
			currentSprite.FlipH = false;
		} else {
			currentSprite.FlipH = true;
		}

		 motion.x = motion.Clamped(MAXSPEED).x;

		if (Input.IsActionPressed("ui_left")) {
			motion.x -= ACCEL;
			facing_right = false;
			animPlayer.Play("walk");
		} else if (Input.IsActionPressed("ui_right")) {
			motion.x += ACCEL;
			facing_right = true;
			animPlayer.Play("walk");
		} else {
			motion = motion.LinearInterpolate(Vector2.Zero, 0.2f);
			animPlayer.Play("idle");
		}

		if (IsOnFloor())
		{
			// On ne regarde qu'un seul fois et non le maintient de la touche
			if (Input.IsActionJustPressed("ui_select")) {
				motion.y = -JUMPFORCE;
				GD.Print($"motion.y = {motion.y}");
				Console.WriteLine($"motion.y = {motion.y}");
		
			}
		}
		if(leftLooker.IsColliding() && !IsOnFloor() && wjleft)
		{
			if (Input.IsActionJustPressed("ui_select")){
				motion.x = REBOUND;
				motion.y = -REBOUND;
				GD.Print($"motion.y = {motion.y}");
				Console.WriteLine($"motion.y = {motion.y}");
				wjleft = false; 
				wjright = true;
				
			}
		}
		if(rightLooker.IsColliding() && !IsOnFloor() && wjright)
		{
			if (Input.IsActionJustPressed("ui_select")){
				motion.x = -REBOUND;
				motion.y = -REBOUND;
				GD.Print($"motion.y = {motion.y}");
				Console.WriteLine($"motion.y = {motion.y}");
				wjright = false;
				wjleft = true;
			
			}
		}		
		if (!IsOnFloor() || can_jump) {
			if (motion.y < 0) {
				animPlayer.Play("jump");
			} else if (motion.y > 0) {
				animPlayer.Play("fall");
			}
			if(rightLooker.IsColliding())
			{
				currentSprite.FlipH = false;
				animPlayer.Play("wallSlide");
			}
			if(leftLooker.IsColliding())
			{
				currentSprite.FlipH = true;
				animPlayer.Play("wallSlide");
			}
		}
		if(IsOnFloor())
		{
			wjleft = true;
			wjright = true;
		}

		motion = MoveAndSlide(motion, UP);
	}


	
}





