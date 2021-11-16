using Godot;
using System;

public class CombatScene : Node2D
{
	public HBoxContainer hand;
	public HBoxContainer rowEnemy;
	public HBoxContainer rowAlie;
	public Card selectedCard;
	public Mana mana;
	public Player_Life enemy_Life;
	public Player_Life player_Life;
	int cardID;

	public override void _Ready()
	{
		cardID = 0;
		mana = GetNode<Mana>("Hud/Mana/Mana");
		enemy_Life = GetNode<Player_Life>("Hud/Enemy_Life");
		enemy_Life.enemy = true;
		player_Life = GetNode<Player_Life>("Hud/Player_Life");
		player_Life.enemy = false;

		rowEnemy = GetNode<HBoxContainer>("Hud/Battlefield/Slots_Contener/Rows/Line1");
		var x = (PackedScene)ResourceLoader.Load("res://Objects/Slot.tscn");
		for (int i = 0;i < 6; i++)
		{
			Slot y = (Slot)x.Instance();
			//int z = GetCardID();
			y.Initialise(i, this, false);
			rowEnemy.AddChild(y);
		}

		rowAlie = GetNode<HBoxContainer>("Hud/Battlefield/Slots_Contener/Rows/Line2");
		var a = (PackedScene)ResourceLoader.Load("res://Objects/Slot.tscn");
		for (int i = 0;i < 6; i++)
		{
			Slot y = (Slot)a.Instance();
			//int z = GetCardID();
			y.Initialise(i, this, true);
			rowAlie.AddChild(y);
		}

		hand = GetNode<HBoxContainer>("Hud/Player_Hand/CenterContainer/HBoxContainer");
		var b = (PackedScene)ResourceLoader.Load("res://Objects/Card.tscn");
		for (int i = 0;i < 10; i++)
		{
			Card y = (Card)b.Instance();
			int z = GetCardID();
			y.Initialise(z, 1, 1, 1, this);
			hand.AddChild(y);
		}
	}

	public Card SlotGetCard()
	{
		var x = (PackedScene)ResourceLoader.Load("res://Objects/Card.tscn");
		Card y = (Card)x.Instance();
		int z = selectedCard.ID;
		int a = selectedCard.manaInt;
		y.Initialise(z, 1, 1, 1, this);
		mana.CardPlaced(a);
		selectedCard.QueueFree();
		selectedCard = null;
		return y;
	}

	public void CardReadyToPair(Card card)
	{
		selectedCard = card;
		GetTree().CallGroup("Slots", "ReadyToPair");
	}

	public void Attack(int damage, int slotID)
	{
		Slot selectedSlot = rowEnemy.GetChild<Slot>(slotID);
		selectedSlot.Attack(damage);
	}

	public void AttackEnemy(int damage)
	{
		enemy_Life.Damaged(damage);
	}

	public int GetCardID()
	{
		int ID = cardID;
		cardID++;
		return ID;
	}

	private void _on_Button_pressed()
	{
		GetTree().CallGroup("Cards", "StartTurn");
		mana.Refill();
	}

	public int GetMana()
	{
		return mana.mana;
	}
}
