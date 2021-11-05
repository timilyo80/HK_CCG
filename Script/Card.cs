using Godot;
using System;

public class Card : Control
{	
	public bool inBattle = false;
	public bool attacked = false;

	// Turn active to false because if mana >= cost then activate
	public bool active = true;
	public bool enemy = false;
	public int ID;

	private Texture txtClicked = (Texture)GD.Load("res://Image/Card_OutLine_Clicked.png");
	private Texture txtHover = (Texture)GD.Load("res://Image/Card_OutLine.png");
	private TextureButton btn;

	public override void _Ready()
	{
		btn = GetNode<TextureButton>("Button");
	}
	
	private void _on_Button_pressed()
	{
		if (active)
		{
			GD.Print("Card Click");
			if (!inBattle)
			{
				btn.TextureNormal = txtClicked;
				GetTree().CallGroup("Slots", "ReadyToPair", this);
				GetTree().CallGroup("Cards", "Deactivate");
			}
			else if (!attacked)
			{
				GD.Print("ATTACK");
				attacked = true;
				GetTree().CallGroup("Cards", "Deactivate");
				GetTree().CallGroup("Cards", "Activate");
				GetTree().CallGroup("Slots", "Attack", ID);
			}
		}
	}

	public void Activate()
	{
		if (!enemy && !attacked)
		{
			this.active = true;
			this.btn.TextureHover = txtHover;
		}
	}

	public void Deactivate()
	{
		this.active = false;
		this.btn.TextureHover = null;
	}
}
