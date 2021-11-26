using Godot;
using System;

public class GameOver : Node2D
{
    AudioStreamPlayer music;
    Manager_InGame manager;

    public override void _Ready()
    {
        manager = GetNode<Manager_InGame>("/root/DeckPlayer");
        music = GetNode<AudioStreamPlayer>("CanvasLayer/Music");
        if (manager.music)
            music.Play();
    }

    private void _on_Button_pressed()
    {
        GetTree().ChangeScene("res://Scene/CombatScene.tscn");
    }

    private void _on_Home_pressed()
    {
        GetTree().ChangeScene("res://Scene/TitleScreen.tscn");
    }
}
