using Godot;
using System;

public class Slot : Control
{
	[Export]
	public bool PlayerLine;
	[Export]
	public int ID;
	
	public bool Active = false;
	public Card filledCard;
	private CombatScene combatScene;
	
	//public TextureButton btn = GetNode<TextureButton>("Button");
	
	public override void _Ready()
	{

	}

	public void Initialise(int id, CombatScene cs, bool playerLine)
	{
		combatScene = cs;
		PlayerLine = playerLine;
		ID = id;

		if (!PlayerLine)
		{
			var x = (PackedScene)ResourceLoader.Load("res://Objects/Card.tscn");
			filledCard = (Card)x.Instance();
			filledCard.Initialise(ID, ID, ID, ID, combatScene);
			filledCard.enemy = true;
			filledCard.Deactivate();
			AddChild(filledCard);
		}
	}
	
	private void _on_TextureButton_pressed()
	{
		if (PlayerLine == true && Active == true)
		{
			filledCard = combatScene.SlotGetCard();
			AddChild(filledCard);
			filledCard.inBattle = true;
			filledCard.slot = this;
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

	public void ReadyToPair()
	{
		this.Activate();
	}

	public void Attacked(int damage)
	{
		if (filledCard != null)
		{
			var x = (PackedScene)ResourceLoader.Load("res://Animation/BaseAttack.tscn");
			BaseAttack y = (BaseAttack)x.Instance();
			AddChild(y);
			filledCard.Damaged(damage);

			if (filledCard.lifeInt == 0)
			{
				filledCard.QueueFree();
				filledCard = null;
			}
		}
		else if (!PlayerLine)
		{
			combatScene.AttackEnemy(damage);
		}
		else
		{
			combatScene.AttackPlayer(damage);
		}
	}
	
}
