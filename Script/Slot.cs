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
	private string[] cardStat;
	private Manager_InGame manager;
	
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
			manager = combatScene.getManager();
			var x = (PackedScene)ResourceLoader.Load("res://Objects/Card.tscn");
			filledCard = (Card)x.Instance();
			cardStat = manager.NewCard(manager.deckEnemy[ID]);
			
			filledCard.Initialise(ID,
				int.Parse(cardStat[0]),
				int.Parse(cardStat[1]),
				int.Parse(cardStat[2]),
				cardStat[3],
				combatScene);

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

	/*public void ReadyToPair()
	{
		this.Activate();
	}*/

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
