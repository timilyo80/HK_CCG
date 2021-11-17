using Godot;
using System;

public class Card_ForEdit : Control
{
    private Texture txtClicked = (Texture)GD.Load("res://Image/Card_OutLine_Clicked.png");
	private Texture txtHover = (Texture)GD.Load("res://Image/Card_OutLine.png");
	private TextureButton btn;
	private Label mana;
	private Label life;
	private Label attack;
    private DeckEditor deckEditor;

    public int ID;
    public int manaInt;
	public int lifeInt;
	public int attackInt;

    public override void _Ready()
    {
        btn = GetNode<TextureButton>("Button");
		mana = GetNode<Label>("Mana");
		life = GetNode<Label>("Life");
		attack = GetNode<Label>("Attack");
    }

    public void Initialise(int id, int m, int a, int l, DeckEditor de)
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
		deckEditor = de;
    }

    public void _on_Button_pressed()
    {
        deckEditor.AddCard(ID);
    }
}
