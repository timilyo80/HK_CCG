using Godot;
using System;

public class DeckEditor : Control
{
    private Manager_InGame manager;
    private GridContainer grid;
    public int[] deckCurrent;
    public int fillIterator = 0;
    private int maxGrid = 8;
    private int minID = 1;
    private int maxID;
    private int state = 0;
    
    public override void _Ready()
    {
        maxID = maxGrid;
        grid = GetNode<GridContainer>("GridContainer");
        var x = (PackedScene)ResourceLoader.Load("res://Objects/Card_ForEdit.tscn");
        manager = GetNode<Manager_InGame>("/root/DeckPlayer");
        deckCurrent = manager.deckAlie;


        for (int i = minID; i <= maxID; i++)
        {
            Card_ForEdit y = (Card_ForEdit)x.Instance();
            int[] z = manager.NewCard(i);
            y.Initialise(i, z[0], z[1], z[2], this);
            grid.AddChild(y);
        }
    }

    public void AddCard(int id)
    {
        deckCurrent[fillIterator] = id;
        fillIterator++;
    }

    private void _on_btn_Start_pressed()
    {
        manager.deckAlie = deckCurrent;
        GetTree().ChangeScene("res://Scene/CombatScene.tscn");
    }
}
