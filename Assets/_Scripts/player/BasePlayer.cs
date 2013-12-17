using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public abstract class BasePlayer<U> : BaseBehaviour<U> where U : class 
{
	public String Name { get; protected set; }
	public int Score { get; protected set; }
	public int ShipCount { get { return ShipRepo.GetAllShips().Where(x => x.Value.active).Count(); } }

	public PlayerAction CurrentAction { get; set; }
	public ShipRepository ShipRepo { get; set; }
	protected PlaceController PlaceController { get { return PlaceController.Current; } }

	protected Boolean _fleatCreated;

	public BasePlayer()
	{
		ShipRepo = new ShipRepository();
		_fleatCreated = false;
	}

	protected abstract void CreateFleat(List<eShipType> types);

	public void SetAction(PlayerAction action)
	{
		CurrentAction = action;
	}

	public void ResetAction()
	{
		CurrentAction = PlayerAction.Wait;
	}

	public Boolean Is(PlayerAction action)
	{
		return CurrentAction == action;
	}
}
