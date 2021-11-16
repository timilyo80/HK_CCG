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
	public Manager_InGame manager;
	private Godot.PackedScene cardTemplate;
	int cardID;
	int[] deck;
	int deckIterator = 0;
	int[] cardStat;
	int maxRow = 6;
	int maxCard = 10;

	public override void _Ready()
	{
		cardID = 0;
		manager = GetNode<Manager_InGame>("/root/DeckPlayer");
		deck = manager.deckAlie;
		mana = GetNode<Mana>("Hud/Mana/Mana");
		enemy_Life = GetNode<Player_Life>("Hud/Enemy_Life");
		enemy_Life.enemy = true;
		player_Life = GetNode<Player_Life>("Hud/Player_Life");
		player_Life.enemy = false;

		rowEnemy = GetNode<HBoxContainer>("Hud/Battlefield/Slots_Contener/Rows/Line1");
		var x = (PackedScene)ResourceLoader.Load("res://Objects/Slot.tscn");
		for (int i = 0;i < maxRow; i++)
		{
			Slot y = (Slot)x.Instance();
			//int z = GetCardID();
			y.Initialise(i, this, false);
			rowEnemy.AddChild(y);
		}

		rowAlie = GetNode<HBoxContainer>("Hud/Battlefield/Slots_Contener/Rows/Line2");
		var a = (PackedScene)ResourceLoader.Load("res://Objects/Slot.tscn");
		for (int i = 0;i < maxRow; i++)
		{
			Slot y = (Slot)a.Instance();
			//int z = GetCardID();
			y.Initialise(i, this, true);
			rowAlie.AddChild(y);
		}

		hand = GetNode<HBoxContainer>("Hud/Player_Hand/CenterContainer/HBoxContainer");
		cardTemplate = (PackedScene)ResourceLoader.Load("res://Objects/Card.tscn");
		for (int i = 0;i < maxCard; i++)
		{
			PickCard();
		}
	}

	public void PickCard()
	{
		if (deckIterator < deck.Length && hand.GetChildCount() < 10)
		{
			Card y = (Card)cardTemplate.Instance();
			int z = GetCardID();
			cardStat = manager.NewCard(deck[deckIterator]);
			deckIterator++;
			y.Initialise(z, cardStat[0], cardStat[1], cardStat[2], this);
			hand.AddChild(y);
		}
	}

	public void CardReadyToPair(Card card)
	{
		selectedCard = card;
		GetTree().CallGroup("Slots", "ReadyToPair");
	}

	public Card SlotGetCard()
	{
		var x = (PackedScene)ResourceLoader.Load("res://Objects/Card.tscn");
		Card y = (Card)x.Instance();
		int z = selectedCard.ID;
		int a = selectedCard.manaInt;
		y.Initialise(z, a, selectedCard.attackInt, selectedCard.lifeInt, this);
		mana.CardPlaced(a);
		selectedCard.QueueFree();
		selectedCard = null;
		return y;
	}

	public void Attack(int damage, Slot slot)
	{
		if (slot.PlayerLine)
			rowEnemy.GetChild<Slot>(slot.ID).Attacked(damage);
		else
			rowAlie.GetChild<Slot>(slot.ID).Attacked(damage);
	}

	public void AttackEnemy(int damage)
	{
		enemy_Life.Damaged(damage);
	}

	public void AttackPlayer(int damage)
	{
		player_Life.Damaged(damage);
	}

	public void EnemyAttack()
	{
		for (int i = 0; i < maxRow; i++)
		{
			Slot selectedRow = rowEnemy.GetChild<Slot>(i);
			if (selectedRow.filledCard != null)
			{
				Attack(selectedRow.filledCard.attackInt, selectedRow);
			}
		}
	}

	public int GetCardID()
	{
		int ID = cardID;
		cardID++;
		return ID;
	}

	private void _on_Button_pressed()
	{
		EnemyAttack();
		PickCard();
		GetTree().CallGroup("Cards", "StartTurn");
		mana.Refill();
	}

	public int GetMana()
	{
		return mana.mana;
	}
}
