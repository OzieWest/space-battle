using System;
using System.Collections.Generic;
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
	public GameObject IconMove { get; set; }

	public PlayerAction Action { get; set; }
	public ShipRepository ShipRepo { get; set; }

	public void Start()
	{
		Current = this;
		ShipRepo = new ShipRepository(this);

		Action = PlayerAction.Wait;

		IconMove = Inst(PrefabFactory.Current.IconMove, Vector3.zero, Quaternion.identity);

		CreateFleat();
	}

	public void OnGUI()
	{
		GUI.Label(new Rect(5, 5, 150, 20), "Name: " + Name);
		GUI.Label(new Rect(5, 25, 150, 20), "Score: " + Score.ToString());
		GUI.Label(new Rect(5, 45, 150, 20), "Action: " + Action.ToString());
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
}
