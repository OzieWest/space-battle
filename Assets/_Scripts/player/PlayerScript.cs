using System;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAction
{
	Undefine,
	Move,
	Attack,
	Stay
}

public class PlayerScript : BaseBehaviour<PlayerScript>
{
	public String Name;
	public int Score;
	public GameObject IconMove { get; set; }

	public PlayerAction Action { get; set; }
	public ShipRepository ShipRepo { get; set; }

	public void Start()
	{
		Current = this;
		ShipRepo = new ShipRepository(this);

		Action = PlayerAction.Undefine;

		IconMove = Inst(PrefabFactory.Current.IconMove, Vector3.zero, Quaternion.identity);

		CreateFleat();
	}

	public void OnGUI()
	{
		GUI.Label(new Rect(5, 5, 150, 20), "Name: " + Name);
		GUI.Label(new Rect(5, 25, 150, 20), "Score: " + Score.ToString());
	}

	public void CreateFleat()
	{
		var types = new List<ShipType>
		{
			ShipType.Small
		};

		foreach (var type in types)
		{
			ShipRepo.CreateShip(type, Vector3.zero);
		}
	}
}
