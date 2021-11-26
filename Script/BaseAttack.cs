using Godot;
using System;

public class BaseAttack : Node2D
{
	Sprite currentSprite;
	AnimationPlayer animPlayer;
	AnimationTree animTree;
	AnimationNodeStateMachinePlayback animState;
	AudioStreamPlayer sound;

	Manager_InGame manager;

	public override void _Ready()
	{
		currentSprite = GetNode<Sprite>("Sprite");
		animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		manager = GetNode<Manager_InGame>("/root/DeckPlayer");
		
		animPlayer.Play("Attack");
		sound = GetNode<AudioStreamPlayer>("Sound");

		if (manager.sound)
			sound.Play();
	}
}
