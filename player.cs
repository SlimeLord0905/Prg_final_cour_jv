using Godot;
using System;

public class player : KinematicBody2D
{

	enum State
	{
		idle,
		run,
		jump,
		fall,
		attack,
		wallslideleft,
		wallslideright,
		death
	}
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

	State currentState = State.idle;




	Vector2 motion = new Vector2();
	Vector2 dir = new Vector2();

	Sprite currentSprite;
	AnimationPlayer animPlayer;
	RayCast2D leftLooker;
	RayCast2D rightLooker;
	AnimationTree animTree;
	AnimationNodeStateMachinePlayback stateMachine;
	CollisionShape2D lefthurtbox;
	CollisionShape2D righthurtbox;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animTree = GetNode<AnimationTree>("AnimationTree");
		currentSprite = GetNode<Sprite>("sprite");
		lefthurtbox = GetNode<CollisionShape2D>("hurtbox/hurtboxShapeLeft");
		righthurtbox = GetNode<CollisionShape2D>("hurtbox/hurtboxShapeRight");
		//animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		leftLooker = GetNode<RayCast2D>("lookLeft");
		rightLooker = GetNode<RayCast2D>("lookRight");
		stateMachine = (AnimationNodeStateMachinePlayback)animTree.Get("parameters/playback");
	}


	public Vector2 getInput()
	{
		var result = new Vector2();
		result.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
		return result;
	}

	public void idle()
	{
		if (dir.x != 0)
		{
			currentState = State.run;
			stateMachine.Travel("walk");
			GD.Print($"going to {currentState}");
		}
		else
		{
			motion = motion.LinearInterpolate(Vector2.Zero, 0.2f);
		}
		if (IsOnFloor())
		{
			// On ne regarde qu'un seul fois et non le maintient de la touche
			if (Input.IsActionJustPressed("ui_select"))
			{

				currentState = State.jump;
				stateMachine.Travel("jump");
				GD.Print($"going to {currentState}");
			}
		}
		if (Input.IsActionJustPressed("ui_attack"))
		{
			currentState = State.attack;
			stateMachine.Start("attack");
			GD.Print($"going to {currentState}");
		}

	}
	private void run()
	{
		apply_movement_x(dir);

		// On ne regarde qu'un seul fois et non le maintient de la touche
		if (Input.IsActionJustPressed("ui_select"))
		{
			currentState = State.jump;
			stateMachine.Travel("jump");
			GD.Print($"going to {currentState}");
		}
		else if (dir.x == 0)
		{
			currentState = State.idle;
			stateMachine.Travel("idle");
			Console.WriteLine($"motion.y = {motion.y}");
		}

		if (motion.y > 0)
		{
			if (motion.y > 0)
			{
				currentState = State.fall;
				stateMachine.Travel("fall");
				GD.Print($"going to {currentState}");
			}
		}
		if (Input.IsActionJustPressed("ui_attack"))
		{
			currentState = State.attack;
			stateMachine.Travel("attack");
			GD.Print($"going to {currentState}");
		}

	}
	private void jump()
	{
		if (IsOnFloor())
		{
			motion.y = -JUMPFORCE;
			GD.Print($"motion.y = {motion.y}");
			Console.WriteLine($"motion.y = {motion.y}");
		}
		if (motion.y > 0)
		{
			currentState = State.fall;
			stateMachine.Travel("fall");
			GD.Print($"going to {currentState}");
		}


	}
	private void fall()
	{
		apply_movement_x(dir);

		if (leftLooker.IsColliding())
		{
			currentState = State.wallslideleft;
			currentSprite.FlipH = true;
			facing_right = false;
			stateMachine.Travel("wallSlide");
			GD.Print($"going to {currentState}");
		}
		if (rightLooker.IsColliding())
		{
			currentState = State.wallslideright;
			currentSprite.FlipH = false;
			facing_right = true;
			stateMachine.Travel("wallSlide");
			GD.Print($"going to {currentState}");
		}
		if (motion.y == 0)
		{
			currentState = State.idle;
			stateMachine.Travel("idle");
			GD.Print($"going to {currentState}");
		}
		if (Input.IsActionJustPressed("ui_attack"))
		{
			currentState = State.attack;
			stateMachine.Travel("attack");
			GD.Print($"going to {currentState}");
		}
	}
	private void attack()
	{
		motion = motion.LinearInterpolate(Vector2.Zero, 0.2f);
		if (facing_right)
		{
			righthurtbox.Disabled = false;
		}
		else
		{
			lefthurtbox.Disabled = false;
		}
	}
	private void EndAttack()
	{
		righthurtbox.Disabled = true;
		lefthurtbox.Disabled = true;
		currentState = State.idle;
		stateMachine.Travel("idle");
		GD.Print($"going to {currentState}");
	}

	public void wallslide(int side)
	{
		if (leftLooker.IsColliding() || rightLooker.IsColliding())
		{
			if (Input.IsActionJustPressed("ui_select"))
			{
				walljump(side);
			}
			if (motion.y == 0)
			{
				currentState = State.idle;
				stateMachine.Travel("idle");
				GD.Print($"going to {currentState}");
			}
		}
		else
		{
			currentState = State.fall;
			stateMachine.Travel("fall");
			GD.Print($"going to {currentState}");
		}

	}
	private void death()
	{
		
	}
	private void limitGravityWallSlide()
	{
		int max_speed = 64;
		if (Input.IsActionPressed("ui_down"))
		{
			max_speed = 6 * 64;
		}

		if (motion.y > max_speed)
		{
			motion.y = max_speed;
		}

	}


	public override void _PhysicsProcess(float delta)
	{
		dir = getInput();
		switch (currentState)
		{
			case State.idle:
				idle();
				break;
			case State.run:
				run();
				break;
			case State.jump:
				jump();
				break;
			case State.fall:
				fall();
				break;
			case State.attack:
				attack();
				break;
			case State.wallslideright:
				wallslide(1);
				break;
			case State.wallslideleft:
				wallslide(2);
				break;
			case State.death:
				death();
				break;
		}
		apply_gravity(delta, GRAVITY);
		if (currentState == State.wallslideleft || currentState == State.wallslideright)
		{
			limitGravityWallSlide();
		}
		motion = MoveAndSlide(motion, UP);
	}

	public void apply_gravity(float delta, int grav)
	{
		motion.y += grav;
		if (motion.y > MAXFALLSPEED)
		{
			motion.y = MAXFALLSPEED;
		}
	}

	public void apply_movement_x(Vector2 dir)
	{
		if (dir.x > 0)
		{
			currentSprite.FlipH = false;
			facing_right = true;
		}
		else if (dir.x < 0)
		{
			currentSprite.FlipH = true;
			facing_right = false;
		}

		motion.x += ACCEL * dir.x;
		motion.x = motion.Clamped(MAXSPEED).x;
	}
	public void walljump(int side)
	{

		if (side == 1)
		{
			motion.x = -MAXSPEED;
		}
		if (side == 2)
		{
			motion.x = MAXSPEED;
		}

		motion.y = -JUMPFORCE;
		GD.Print($"motion.y = {motion.y}");
		Console.WriteLine($"motion.y = {motion.y}");

	}
	
private void _on_hitbox_area_entered(object area)
{
	motion = motion.LinearInterpolate(Vector2.Zero, 0.2f);
	currentState = State.death;
	stateMachine.Travel("death");
	GD.Print($"going to {currentState}");
}
private void die()
{
	QueueFree();
	GetTree().ReloadCurrentScene();
	GetTree().ChangeScene("res://GameOver.tscn");
	
}


}
