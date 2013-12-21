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

public class Player : BasePlayer<Player>
{
	public Player() { Current = this; Name = "Player [one]"; }

	public void Start()
	{
		ResetAction();
	}

	public void Update()
	{
		if (!_fleatCreated)
		{
			var types = new List<eShipType>
			{
				eShipType.Small,
				eShipType.Medium,
				eShipType.Big
			};

			this.CreateFleat(types);
		}
	}

	public void OnGUI()
	{
		var coor = PlaceController.GetCoordinateLastRow();
		coor.y *= -1;

		var screenCoor = Camera.main.WorldToScreenPoint(coor);
		screenCoor.x -= 20;
		screenCoor.y += 25;

		var widthLabel = 150;
		var heightLabel = 20;

		var ships = ShipRepo.GetAllShips();

		if (_fleatCreated)
		{
			GUI.Label(new Rect(screenCoor.x, screenCoor.y, widthLabel, heightLabel),
				"Ship small: " + ships[eShipType.Small].State);
			GUI.Label(new Rect(screenCoor.x, screenCoor.y + 20, widthLabel, heightLabel),
				"Ship middle: " + ships[eShipType.Medium].State);
			GUI.Label(new Rect(screenCoor.x, screenCoor.y + 40, widthLabel, heightLabel),
				"Ship big: " + ships[eShipType.Big].State);
		}
	}

	protected override void CreateFleat(List<eShipType> types)
	{
		foreach (var type in types)
		{
			var freePlace = PlaceController.GetRandomPlace();
			freePlace.Close();
			ShipRepo.CreateShip(type, freePlace.Position);
		}

		_fleatCreated = true;
	}
}
