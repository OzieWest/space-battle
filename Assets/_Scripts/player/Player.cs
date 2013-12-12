using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum PlayerAction
{
	Wait,
	Move,
	Attack,
	Stay
}

public class Player : BaseBehaviour<Player>
{
	public String Name;
	public int Score;
	public int ShipCount { get { return ShipRepo.GetAllShips().Where(x => x.Value.active).Count(); } }

	public PlayerAction Action { get; set; }
	public ShipRepository ShipRepo { get; set; }
	public PlaceController PlaceController { get { return PlaceController.Current; } }

	private Boolean fleatCreated = false;

	public void Start()
	{
		Current = this;
		ShipRepo = new ShipRepository(this);

		Action = PlayerAction.Wait;
	}

	public void Update()
	{
		if (!fleatCreated)
			CreateFleat();
	}


	public void OnGUI()
	{
		const int widthLabel = 150;
		const int heightLabel = 20;

		var offset = 5;
		var position = Screen.height - 60;
		const int widthButton = 50;
		const int heightButton = 50;

		//GUI.Label(new Rect(offset, 5, widthLabel, heightLabel), "Name: " + Name);
		//GUI.Label(new Rect(offset, 25, widthLabel, heightLabel), "Score: " + Score);
		//GUI.Label(new Rect(offset, 45, widthLabel, heightLabel), "Action: " + Action);
		//GUI.Label(new Rect(offset, 65, widthLabel, heightLabel), "ShipCount: " + ShipCount);
	}

	public void CreateFleat()
	{
		var types = new List<ShipType>
		{
			ShipType.Small,
			ShipType.Medium,
			ShipType.Big
		};

		foreach (var type in types)
		{
			var freePlace = PlaceController.GetRandomLocation();
			freePlace.Close();
			ShipRepo.CreateShip(type, freePlace.Position);
		}

		fleatCreated = true;
	}

	public void SetAction(PlayerAction action)
	{
		Action = action;
	}

	public void ResetAction()
	{
		Action = PlayerAction.Wait;
	}

	public Boolean Is(PlayerAction action)
	{
		return Action == action;
	}
}
