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

public class PlayerScript : BaseBehaviour<PlayerScript>
{
	public String Name;
	public int Score;
	public int ShipCount 
	{
		get { return ShipRepo.GetAllShips().Where(x => x.Value.active).Count(); }
	}

	public PlayerAction Action { get; set; }
	public ShipRepository ShipRepo { get; set; }
	
	public void Start()
	{
		Current = this;
		ShipRepo = new ShipRepository(this);

		Action = PlayerAction.Wait;

		CreateFleat();
	}

	public void OnGUI()
	{
		const int offset = 5;
		const int widthLabel = 150;
		const int heightLabel = 20;

		GUI.Label(new Rect(offset, 5, widthLabel, heightLabel), "Name: " + Name);
		GUI.Label(new Rect(offset, 25, widthLabel, heightLabel), "Score: " + Score);
		GUI.Label(new Rect(offset, 45, widthLabel, heightLabel), "Action: " + Action);
		GUI.Label(new Rect(offset, 65, widthLabel, heightLabel), "ShipCount: " + ShipCount);

		var position = 85;
		const int widthButton = 35;
		const int heightButton = 35;

		foreach (var ship in ShipRepo.GetAllShips().Where(ship => !ship.Value.active))
		{
			if (GUI.Button(new Rect(offset, position, widthButton, heightButton), GetImageByType(ship.Key)))
			{
				ship.Value.transform.position = GridScript.Current.GetRandomLocation().Position;
				ship.Value.active = true;
			}

			position += 40;
		}
	}

	public void CreateFleat()
	{
		var types = new List<ShipType>
		{
			ShipType.Small,
			ShipType.Medium
		};

		foreach (var type in types)
			ShipRepo.CreateShip(type, Vector3.zero);
	}

	public void SetAction(PlayerAction action)
	{
		Action = action;
	}

	public void ResetAction()
	{
		Action = PlayerAction.Wait;
	}

	public Boolean IsActionMove()
	{
		return Action == PlayerAction.Move;
	}

	public Boolean IsActionAttack()
	{
		return Action == PlayerAction.Attack;
	}

	public Boolean IsActionStay()
	{
		return Action == PlayerAction.Stay;
	}

	private Texture2D GetImageByType(ShipType type)
	{
		switch (type)
		{
			case ShipType.Small:
				return IFactory.SmallShipPicture;
			case ShipType.Medium:
				return IFactory.MediumShipPicture;
			case ShipType.Big:
				return IFactory.BigShipPicture;
		}

		return null;
	}
}
