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
			var types = new List<ShipType>
			{
				ShipType.Small,
				ShipType.Medium,
				ShipType.Big
			};

			this.CreateFleat(types);
		}
	}

	protected override void CreateFleat(List<ShipType> types)
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
