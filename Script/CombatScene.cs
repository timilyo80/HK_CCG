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
	private AudioStreamPlayer music;
	private Label forTest;
	int cardID;
	int[] deck;
	int deckIterator = 0;
	string[] cardStat;
	int maxRow = 6;
	int maxCard = 10;

	public override void _Ready()
	{
		cardID = 0;
		manager = GetNode<Manager_InGame>("/root/DeckPlayer");
		deck = manager.deckAlie;
		mana = GetNode<Mana>("Hud/Mana/Mana");
		music = GetNode<AudioStreamPlayer>("Music");
		enemy_Life = GetNode<Player_Life>("Hud/Enemy_Life");
		enemy_Life.enemy = true;
		player_Life = GetNode<Player_Life>("Hud/Player_Life");
		player_Life.enemy = false;
		forTest = GetNode<Label>("Hud/Label");
		if (!manager.test)
			forTest.Visible = false;

		if (manager.music)
			music.Play();

		rowEnemy = GetNode<HBoxContainer>("Hud/Battlefield/Slots_Contener/Rows/Line1");
		var x = (PackedScene)ResourceLoader.Load("res://Objects/Slot.tscn");
		for (int i = 0;i < maxRow; i++)
		{
			Slot y = (Slot)x.Instance();
			//int z = GetCardID();
			y._Ready();
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
			do
			{
				cardStat = manager.NewCard(deck[deckIterator]);
				deckIterator++;
			} while(deckIterator < deck.Length && deck[deckIterator-1] == 0);

			if (deckIterator < deck.Length)
			{
				Card y = (Card)cardTemplate.Instance();
				int z = GetCardID();
				y.Initialise(z, int.Parse(cardStat[0]),
					int.Parse(cardStat[1]),
					int.Parse(cardStat[2]),
					cardStat[3],
					this);
				hand.AddChild(y);
			}
		}
	}

	public void CardReadyToPair(Card card)
	{
		selectedCard = card;
		GetTree().CallGroup("Slots", "Activate");
		forTest.Text = "Selected Card:\nid: " + selectedCard.ID
			+ "\nMana Cost: " + selectedCard.manaInt
			+ "\nAttack: " + selectedCard.attackInt
			+ "\nLife: " + selectedCard.lifeInt;
	}

	public void UnreadyToPair()
	{
		selectedCard = null;
		GetTree().CallGroup("Slots", "Deactivate");
		forTest.Text = "Selected Card:";
	}

	public Card SlotGetCard()
	{
		var x = (PackedScene)ResourceLoader.Load("res://Objects/Card.tscn");
		Card y = (Card)x.Instance();

		y.Initialise(selectedCard.ID,
			selectedCard.manaInt,
			selectedCard.attackInt,
			selectedCard.lifeInt,
			selectedCard.imageStr,
			this);

		mana.CardPlaced(selectedCard.manaInt);
		selectedCard.QueueFree();
		selectedCard = null;
		forTest.Text = "Selected Card:";
		return y;
	}

	public void Attack(int damage, Slot slot)
	{
		if (slot.PlayerLine)
		{
			rowEnemy.GetChild<Slot>(slot.ID).Attacked(damage);
			forTest.Text = "Selected Card: ATTACK!!!\nDamage: " + damage;
		}
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

	public Manager_InGame getManager()
	{
		return manager;
	}

}
