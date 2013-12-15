using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class EnemyBot : BasePlayer<EnemyBot>
{
	public EnemyBot() { Current = this; Name = "Player [bot one]"; }

	void Start()
	{
		ResetAction();
	}

	void Update()
	{
		if (!_fleatCreated)
		{
			var types = new List<ShipType>
			{
				ShipType.Small
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
