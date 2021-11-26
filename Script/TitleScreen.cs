using Godot;
using System;

public class TitleScreen : Node2D
{
    CenterContainer centerContainer;
    Manager_InGame manager;
    AudioStreamPlayer music;
    Button musicBTN;
    Button soundBTN;
    Button testBTN;
    Button tutorial;
    bool tutorialBOOL = false;
    TextureRect tuto1;
    TextureRect tuto2;

    public override void _Ready()
    {
        centerContainer = GetNode<CenterContainer>("CanvasLayer/CenterContainer");
        manager = GetNode<Manager_InGame>("/root/DeckPlayer");
        music = GetNode<AudioStreamPlayer>("Music");
        testBTN = GetNode<Button>("CanvasLayer/Test");
        tutorial = GetNode<Button>("CanvasLayer/Tutorial");
        musicBTN = GetNode<Button>("CanvasLayer/Music");
        soundBTN = GetNode<Button>("CanvasLayer/Sound");
        tuto1 = GetNode<TextureRect>("CanvasLayer/TextureRect3");
        tuto2 = GetNode<TextureRect>("CanvasLayer/TextureRect2");

        if (manager.music)
        {
            music.Play();
        }

        if (manager.music)
            musicBTN.Text = "Music: ON";
        else
            musicBTN.Text = "Music: OFF";

        if (manager.sound)
            soundBTN.Text = "Sound: ON";
        else
            soundBTN.Text = "Sound: OFF";

        if (manager.test)
            testBTN.Text = "Test Text: ON";
        else
            testBTN.Text = "Test Text: OFF";
    }

    private void _on_Button_pressed()
    {
        centerContainer.Visible = true;
        var x = (PackedScene)ResourceLoader.Load("res://Objects/DeckEditor.tscn");
		DeckEditor y = (DeckEditor)x.Instance();
	    centerContainer.AddChild(y);
    }

    private void _on_Sound_pressed()
    {
        if (manager.sound)
        {
            manager.sound = false;
            soundBTN.Text = "Sound: OFF";
        }
        else
        {
            manager.sound = true;
            soundBTN.Text = "Sound: ON";
        }
    }

    private void _on_Music_pressed()
    {
        if (manager.music)
        {
            manager.music = false;
            music.Stop();
            musicBTN.Text = "Music: OFF";
        }
        else
        {
            manager.music = true;
            music.Play();
            musicBTN.Text = "Music: ON";
        }
    }

    private void _on_Test_pressed()
    {
        manager.test = !manager.test;
        if (manager.test)
            testBTN.Text = "Test Text: ON";
        else
            testBTN.Text = "Test Text: OFF";
    }

    private void _on_Tutorial_pressed()
    {
        tutorialBOOL = !tutorialBOOL;

        if (tutorialBOOL)
        {
            tuto1.Visible = true;
            tuto2.Visible = true;
            tutorial.Text = "Test Text: ON";
        }
        else
        {
            tuto1.Visible = false;
            tuto2.Visible = false;
            tutorial.Text = "Test Text: OFF";
        }
    }
}
