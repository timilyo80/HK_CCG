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
	public int manaInt;
	public int lifeInt;
	public int attackInt;
	public Slot slot;

	private Texture txtClicked = (Texture)GD.Load("res://Image/Card_OutLine_Clicked.png");
	private Texture txtHover = (Texture)GD.Load("res://Image/Card_OutLine.png");
	private TextureButton btn;
	private Label mana;
	private Label life;
	private Label attack;
	private CombatScene combatScene;

	public override void _Ready()
	{
		btn = GetNode<TextureButton>("Button");
		mana = GetNode<Label>("Mana");
		life = GetNode<Label>("Life");
		attack = GetNode<Label>("Attack");
	}

	public void Initialise(int id, int m, int a, int l, CombatScene cs)
	{
		btn = GetNode<TextureButton>("Button");
		mana = GetNode<Label>("Mana");
		life = GetNode<Label>("Life");
		attack = GetNode<Label>("Attack");

		ID = id;
		manaInt = m;
		attackInt = a;
		lifeInt = l;
		mana.Text = m.ToString();
		attack.Text = a.ToString();
		life.Text = l.ToString();
		combatScene = cs;
	}
	
	private void _on_Button_pressed()
	{
		if (active)
		{
			if (!inBattle && combatScene.GetMana() >= manaInt)
			{
				btn.TextureNormal = txtClicked;
				combatScene.CardReadyToPair(this);
				GetTree().CallGroup("Cards", "Deactivate");
			}
			else if (inBattle && !attacked)
			{
				attacked = true;
				GetTree().CallGroup("Cards", "Deactivate");
				GetTree().CallGroup("Cards", "Activate");
				combatScene.Attack(attackInt, slot);
			}
		}
	}

	public void Damaged(int damage)
	{
		lifeInt -= damage;
		if (lifeInt < 0)
			lifeInt = 0;
		life.Text = lifeInt.ToString();
	}

	public void StartTurn()
	{
		this.attacked = false;
		this.Activate();
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
