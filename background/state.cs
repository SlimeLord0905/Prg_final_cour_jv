using Godot;
using System;



public class state : Node
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	private int currentState { get; set; }
	private int previousState = 0;
	KinematicBody2D parent;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		currentState = 1;
		parent = GetNode<KinematicBody2D>("player");
	}

	public override void _PhysicsProcess(float delta)
	{
		if (currentState != null)
		{
			state_logic(delta);
			{
				int transition = get_transition(delta);
				if (transition != 0)
				{
					set_state(transition);
				}
			}
		}
	}

	public void state_logic(float delta)
	{

	}
	public int get_transition(float delta)
	{
		return 0;
	}

	public void enter_state(int oldState, int newState)
	{

	}
	public void exit_state(int oldState, int newState)
	{

	}

	public void set_state(int newState)
	{
		previousState = currentState;
		currentState = newState;


		if (previousState != null)
		{
			exit_state(previousState, currentState);
		}
		if (newState != null)
		{
			enter_state(previousState, currentState);
		}
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	//  public override void _Process(float delta)
	//  {
	//      
	//  }
}
