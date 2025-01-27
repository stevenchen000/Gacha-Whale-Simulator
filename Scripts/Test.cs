using Godot;
using System;
using System.IO;
using System.Net.Http;

public partial class Test : Node
{
	
	public void OnClick(Node viewport, InputEvent @event, int idx)
	{
		Utils.Print(this, "clicked");
	}


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		//client = new System.Net.Http.HttpClient();
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public void TestFunc()
    {
		
	}

}
