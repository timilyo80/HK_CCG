using Godot;
using System;

public class DeckEditor : Control
{
    private Manager_InGame manager;
    private GridContainer grid;
    private Label deck_nmb;
    private Label grid_nmb;
    public int[] deckCurrent;
    public int fillIterator = 0;
    private int maxGrid = 8;
    private int minID = 1;
    private int maxID;
    private int state = 0;
    public bool showAllCards = true;
    
    public override void _Ready()
    {
        maxID = maxGrid;
        grid = GetNode<GridContainer>("GridContainer");
        var x = (PackedScene)ResourceLoader.Load("res://Objects/Card_ForEdit.tscn");
        manager = GetNode<Manager_InGame>("/root/DeckPlayer");
        deck_nmb = GetNode<Label>("Deck_NMB");
        grid_nmb = GetNode<Label>("Grid_NMB");
        grid_nmb.Text = (minID.ToString() + "-" + maxID.ToString());
        deckCurrent = new int[manager.deckAlie.Length];
        manager.deckAlie.CopyTo(deckCurrent, 0);
        SetupIterator();


        for (int i = minID; i <= maxID; i++)
        {
            Card_ForEdit y = (Card_ForEdit)x.Instance();
            string[] z = manager.NewCard(i);
            y.Initialise(i,
                int.Parse(z[0]),
                int.Parse(z[1]),
                int.Parse(z[2]),
                z[3],
                this);
            grid.AddChild(y);
        }
    }

    public void AddCard(int id)
    {
        if (fillIterator < deckCurrent.Length)
        {
            deckCurrent[fillIterator] = id;
            fillIterator++;
            deck_nmb.Text = (fillIterator.ToString() + "/" + deckCurrent.Length.ToString());
        }
    }

    public void RemoveCard(int id)
    {
        int i = 0;
        int y = 0;

        while (i < deckCurrent.Length)
        {
            if (deckCurrent[i] == id && y == 0)
            {
                deckCurrent[i] = 0;
                y = 1;
            }
            else if (y == 1)
            {
                deckCurrent[i-1] = deckCurrent[i];
                deckCurrent[i] = 0;
            }

            i++;
        }

        if (y == 1)
        {
            fillIterator--;
            deck_nmb.Text = (fillIterator.ToString() + "/" + deckCurrent.Length.ToString());
            LoadGrid();
        }
    }

    public void SetupIterator()
    {
        for (int i = 0; i < deckCurrent.Length; i++)
        {
            if (deckCurrent[i] != 0)
                fillIterator++;
        }
        deck_nmb.Text = (fillIterator.ToString() + "/" + deckCurrent.Length.ToString());
    }

    public void LoadGrid()
    {
        grid_nmb.Text = (minID.ToString() + "-" + maxID.ToString());

        for (int i = minID-1; i <= maxID-1; i++)
        {
            if (i < deckCurrent.Length && deckCurrent[i] != 0)
            {
                grid.GetChild<Card_ForEdit>(i-minID+1).Visible = true;

                string[] z = manager.NewCard(deckCurrent[i]);
                grid.GetChild<Card_ForEdit>(i-minID+1).ChangeInfo(deckCurrent[i],
                    int.Parse(z[0]),
                    int.Parse(z[1]),
                    int.Parse(z[2]),
                    z[3]);
            }
            else
            {
                grid.GetChild<Card_ForEdit>(i-minID+1).Visible = false;
            }
        }
    }

    public void LoadGrid_AllCards()
    {
        grid_nmb.Text = (minID.ToString() + "-" + maxID.ToString());

        for (int i = minID; i <= maxID; i++)
        {
            if (i < manager.data_length)
            {
                grid.GetChild<Card_ForEdit>(i-minID).Visible = true;

                string[] z = manager.NewCard(i);
                grid.GetChild<Card_ForEdit>(i-minID).ChangeInfo(i,
                    int.Parse(z[0]),
                    int.Parse(z[1]),
                    int.Parse(z[2]),
                    z[3]);
            }
            else
            {
                grid.GetChild<Card_ForEdit>(i-minID).Visible = false;
            }
        }
    }

    public void Setup_Deck()
    {
        RandomNumberGenerator rng = new RandomNumberGenerator();
        int i;
        int num;
        int nValidCards = 0;

        for (i = 0; i < manager.deckAlie.Length; i++)
            manager.deckAlie[i] = 0;

        for (i = 0; i < deckCurrent.Length; i++)
        {
            if (deckCurrent[i] != 0)
                nValidCards++;
        }

        for (i = 0; i < deckCurrent.Length; i++)
        {
            if (deckCurrent[i] != 0)
            {
                rng.Randomize();
                do
                {
                    num = rng.RandiRange(0, nValidCards-1);
                } while(manager.deckAlie[num] != 0);
                manager.deckAlie[num] = deckCurrent[i];
            }
        }
    }

    private void _on_btn_Start_pressed()
    {
        Setup_Deck();
        GetTree().ChangeScene("res://Scene/CombatScene.tscn");
    }

    private void _on_btn_Left_pressed()
    {
        minID -= maxGrid;
        maxID -= maxGrid;
        int x;

        if (showAllCards)
            x = manager.data_length;
        else
            x = deckCurrent.Length;

        if (minID <= 0)
        {
            maxID = x + (maxGrid - x % maxGrid);
            minID = maxID - maxGrid + 1;
        }

        if (showAllCards)
            LoadGrid_AllCards();
        else
            LoadGrid();
    }

    private void _on_btn_Right_pressed()
    {
        minID += maxGrid;
        maxID += maxGrid;
        int x;

        if (showAllCards)
            x = manager.data_length;
        else
            x = deckCurrent.Length;

        if (minID >= x)
        {
            minID = 1;
            maxID = maxGrid;
        }

        if (showAllCards)
            LoadGrid_AllCards();
        else
            LoadGrid();
    }

    private void _on_btn_Edit_pressed()
    {
        minID = 1;
        maxID = maxGrid;
        showAllCards = true;
        LoadGrid_AllCards();
    }

    private void _on_btn_Deck_pressed()
    {
        minID = 1;
        maxID = maxGrid;
        showAllCards = false;
        LoadGrid();
    }

    private void _on_btn_Preset_pressed()
    {

    }
}
