using Godot;
using System;

public class Player_Life : Control
{
    int maxLife = 20;
    int life = 20;
    Label label;
    public bool enemy;
    public override void _Ready()
    {
        label = GetNode<Label>("Label");
        label.Text = life.ToString();
    }

    public void Damaged(int damage)
    {
        life -= damage;
        if (life < 0)
            life = 0;
        label.Text = life.ToString();

        if (life == 0)
        {
            if (enemy)
                GetTree().ChangeScene("res://Scene/GameWinScene.tscn");
            else
                GetTree().ChangeScene("res://Scene/GameOverScene.tscn");

        }
    }
}
