using Godot;
using System;

public class Card_Button : TextureButton
{
	public override void _Ready()
	{
		
	}

	private void _on_TextureButton_pressed()
	{
		GD.Print("Card Click");
	}

}
