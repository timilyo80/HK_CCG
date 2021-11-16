using Godot;
using System;

public class TitleScreen : Node2D
{
    public override void _Ready()
    {
        
    }

    private void _on_Button_pressed()
    {
	    GetTree().ChangeScene("res://Scene/CombatScene.tscn");
    }
}
