using Godot;
using System;

public class BaseAttack : Node2D
{
	Sprite currentSprite;
	AnimationPlayer animPlayer;
	AnimationTree animTree;
	AnimationNodeStateMachinePlayback animState;

	public override void _Ready()
	{
		currentSprite = GetNode<Sprite>("Sprite");
		animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		
		animPlayer.Play("Attack");
	}
}
