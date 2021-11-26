using Godot;
using System;

public class Mana : Node2D
{
    public int mana = 0;
    public int maxMana = 1;
    private int state = 2;
    Sprite currentSprite;
    AnimationPlayer animPlayer;
    Label label;
    public override void _Ready()
    {
        currentSprite = GetNode<Sprite>("Sprite");
        animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        label = GetNode<Label>("Label");
        mana = maxMana;
        label.Text = mana.ToString();
    }

    public void CardPlaced(int cost)
    {
        mana -= cost;

        if (mana == 0 && state > 0)
        {   
            animPlayer.Play("Empty");
            state = 0;
        }
        else if (mana <= maxMana / 2 && state > 1)
        {
            animPlayer.Play("Half");
            state = 1;
        }

        label.Text = mana.ToString();
    }

    public void Refill()
    {
        if (maxMana < 10)
            maxMana++;
        mana = maxMana;
        label.Text = mana.ToString();
        animPlayer.Play("Full");
        state = 2;
    }
}
