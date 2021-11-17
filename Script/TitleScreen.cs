using Godot;
using System;

public class TitleScreen : Node2D
{
    CenterContainer centerContainer;

    public override void _Ready()
    {
        centerContainer = GetNode<CenterContainer>("CanvasLayer/CenterContainer");
    }

    private void _on_Button_pressed()
    {
        centerContainer.Visible = true;
        var x = (PackedScene)ResourceLoader.Load("res://Objects/DeckEditor.tscn");
		DeckEditor y = (DeckEditor)x.Instance();
	    centerContainer.AddChild(y);
    }
}
