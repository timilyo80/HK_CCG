using Godot;
using System;

public class Manager_InGame : Node
{
    private string pathJSON = "res://Data/CardsDatabase.json";
    private object data_item;
    public int[] deckAlie = new int[] {0, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 1, 2, 3, 4, 5};
    private Godot.Collections.Dictionary card;
    private Godot.Collections.Dictionary cardStats;

	public override void _Ready()
    {        
        File data_file = new File();
        data_file.Open(pathJSON, File.ModeFlags.Read);
        JSONParseResult data_json = JSON.Parse(data_file.GetAsText());
        data_file.Close();
        data_item = data_json.Result;
    }

    public int[] NewCard(int ID)
    {
        card = (Godot.Collections.Dictionary)data_item;
        cardStats = (Godot.Collections.Dictionary)card[ID.ToString()];
        return new int[] {int.Parse(cardStats["Mana"].ToString()), int.Parse(cardStats["Attack"].ToString()), int.Parse(cardStats["Life"].ToString())};
    }
}
