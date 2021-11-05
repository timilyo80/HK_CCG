using Godot;
using System;

public class Slot_TextureButton : TextureButton
{
	[Export]
	public bool PlayerLine = true;
	
	public override void _Ready()
	{
		
	}

	private void _on_TextureButton_pressed()
	{
		if (PlayerLine == true)
		{
			//GD.Print("Slot Clicked");
		}
	}

}
