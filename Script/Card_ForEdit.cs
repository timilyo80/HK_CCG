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
	private TextureRect image;
    private DeckEditor deckEditor;

    public int ID;
    public int manaInt;
	public int lifeInt;
	public int attackInt;
	public string imageStr;

    public override void _Ready()
    {
        btn = GetNode<TextureButton>("Button");
		mana = GetNode<Label>("Mana");
		life = GetNode<Label>("Life");
		attack = GetNode<Label>("Attack");
    }

    public void Initialise(int id, int m, int a, int l, string i, DeckEditor de)
    {
        btn = GetNode<TextureButton>("Button");
		mana = GetNode<Label>("Mana");
		life = GetNode<Label>("Life");
		attack = GetNode<Label>("Attack");
		image = GetNode<TextureRect>("Image");

        ID = id;
		manaInt = m;
		attackInt = a;
		lifeInt = l;
		imageStr = i;
		mana.Text = m.ToString();
		attack.Text = a.ToString();
		life.Text = l.ToString();
		image.Texture = (Texture)GD.Load("res://Cards/Cards_Images/" + imageStr + ".png");
		deckEditor = de;
    }

	public void ChangeInfo(int id, int m, int a, int l, string i)
	{
		ID = id;
		manaInt = m;
		attackInt = a;
		lifeInt = l;
		imageStr = i;
		mana.Text = m.ToString();
		attack.Text = a.ToString();
		life.Text = l.ToString();
		image.Texture = (Texture)GD.Load("res://Cards/Cards_Images/" + imageStr + ".png");
	}

    public void _on_Button_pressed()
    {
		if (deckEditor.showAllCards)
        	deckEditor.AddCard(ID);
		else
			deckEditor.RemoveCard(ID);
    }
}
