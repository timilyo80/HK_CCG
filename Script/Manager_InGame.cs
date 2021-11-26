using Godot;
using System;

public class Manager_InGame : Node
{
    private string pathJSON = "res://Data/CardsDatabase.json";
    private object data_item;
    public int data_length;
    public int[] deckAlie = new int[20]; //{0, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 1, 2, 3, 4, 5};
    public int[] deckEnemy = new int[] {11, 3, 2, 13, 1, 4};
    private Godot.Collections.Dictionary card;
    private Godot.Collections.Dictionary cardStats;

    public bool music = true;
    public bool sound = true;
    public bool test = false;

	public override void _Ready()
    {        
        File data_file = new File();
        data_file.Open(pathJSON, File.ModeFlags.Read);
        JSONParseResult data_json = JSON.Parse(data_file.GetAsText());
        data_file.Close();
        data_item = data_json.Result;
        card = (Godot.Collections.Dictionary)data_item;
        data_length = card.Count;
    }

    public string[] NewCard(int ID)
    {
        cardStats = (Godot.Collections.Dictionary)card[ID.ToString()];
        return new string[] {cardStats["Mana"].ToString(),
            cardStats["Attack"].ToString(),
            cardStats["Life"].ToString(),
            cardStats["Image"].ToString()};
    }
}
