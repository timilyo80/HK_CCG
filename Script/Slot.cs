using Godot;
using System;

public class Slot : Control
{
	[Export]
	public bool PlayerLine;
	[Export]
	public int ID;
	
	public bool Active = false;
	public Card selectedCard;
	public Card filledCard;
	
	//public TextureButton btn = GetNode<TextureButton>("Button");
	
	public override void _Ready()
	{
		if (!PlayerLine)
		{
			var x = (PackedScene)ResourceLoader.Load("res://Objects/Card.tscn");
			filledCard = (Card)x.Instance();
			AddChild(filledCard);
			filledCard.enemy = true;
			filledCard.active = false;
		}
	}
	
	private void _on_TextureButton_pressed()
	{
		if (PlayerLine == true && Active == true && selectedCard != null)
		{
			GD.Print("Slot Clicked");
			var x = (PackedScene)ResourceLoader.Load("res://Objects/Card.tscn");
			filledCard = (Card)x.Instance();
			AddChild(filledCard);
			filledCard.inBattle = true;
			filledCard.ID = this.ID;
			selectedCard.QueueFree();
			GetTree().CallGroup("Slots", "Deactivate");
			GetTree().CallGroup("Cards", "Activate");
		}
	}
	
	public void Activate()
	{
		this.Active = true;
	}
	
	public void Deactivate()
	{
		this.Active = false;
	}

	public void ReadyToPair(Card card)
	{
		this.selectedCard = card;
		this.Activate();
	}

	public void Attack(int id)
	{
		if (!PlayerLine && ID == id)
		if (true)
		{
			var x = (PackedScene)ResourceLoader.Load("res://Animation/BaseAttack.tscn");
			BaseAttack y = (BaseAttack)x.Instance();
			AddChild(y);
			filledCard.QueueFree();
		}
	}
	
}
