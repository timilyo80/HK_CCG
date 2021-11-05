using Godot;
using System;

public class Manager_InGame : Node
{
	private Control selectedCard;
	
	public override void _Ready()
	{
		
	}
	
	public void SelectCard(Control selected)
	{
		GD.Print("OWWWWWO");
		selectedCard = selected;
		GetTree().CallGroup("Slots", "Activate");
	}
	
	public void ChoseSlot(Control chose)
	{
		Vector2 x = chose.RectPosition;
		selectedCard.SetPosition(x);
	}
}
