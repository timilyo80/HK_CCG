using Godot;
using System;

public class BaseAttack : Node2D
{
	Sprite currentSprite;
	AnimationPlayer animPlayer;
	AnimationTree animTree;
	AnimationNodeStateMachinePlayback animState;
	AudioStreamPlayer sound;

	public override void _Ready()
	{
		currentSprite = GetNode<Sprite>("Sprite");
		animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		
		animPlayer.Play("Attack");
		sound = GetNode<AudioStreamPlayer>("Sound");
		sound.Play();
	}
}
