using Godot;
using System;

public class bat : Node2D
	// Declare member variables here. Examples:
{
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.

	bool can_see_player;
	AnimatedSprite sprite;
	public override void _Ready()
	{
		sprite = GetNode<AnimatedSprite>("bat/AnimatedSprite");
	}
	public override void _PhysicsProcess(float delta)
	{
		if(can_see_player)
		{
			var player = GetNode<player>("../player");
			float speed = 200; // in pixels per second
			float moveAmount = speed * delta;
			Vector2 moveDirection = (player.Position - Position).Normalized();
			if(moveDirection.x > 0)
			{
				sprite.FlipH = false;
			}
			else
			{
				sprite.FlipH = true;
			}
			Position += moveDirection * moveAmount;
		}
		
	}
	private void _on_searchArea_area_entered(object area)
	{
		GD.Print("enter");
		can_see_player = true;
	}
	private void _on_searchArea_area_exited(object area)
	{
		GD.Print("exit");
		can_see_player = false;
	}
	

	
	private void _on_hurtbox_area_entered(object area)
	{
		QueueFree();
	}
}









