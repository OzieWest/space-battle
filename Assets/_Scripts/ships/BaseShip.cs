using UnityEngine;
using System.Collections;
using System;

public enum eShipType
{
	Small,
	Medium,
	Big
}

public enum eShipState
{
	Wait,
	Select,
	Move,
	Attack,
	Explode,
	Rest
}

public class BaseShip<U> : BaseBehaviour<U> where U: class
{
	#region Struct
	public eShipType CurrentType { get; set; }
	public eShipState CurrentState { get; set; }
	public int Health { get; set; }
	public int Power { get; set; }
	#endregion

	protected Vector3 _targetPosition;
	protected Vector3 _endPosition;
	protected float _moveSpeed;

	protected Boolean IsSelectable { get; set; }

	public Rect WindowGui { get; set; }

	public BaseShip()
	{
		IsSelectable = true;
		_moveSpeed = 4f;

		WindowGui = new Rect(5, 80, 150, 400);
	}
}
