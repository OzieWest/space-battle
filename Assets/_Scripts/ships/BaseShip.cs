using UnityEngine;
using System.Collections;
using System;

public class BaseShip<U> : BaseBehaviour<U> where U: class
{
	#region Struct
	public ShipType Type { get; set; }
	public ShipAction Action { get; set; }
	public ShipState State { get; set; }
	public int Health { get; set; }
	public int Power { get; set; }
	#endregion

	protected Vector3 _targetPosition;
	protected Vector3 _endPosition;
	protected float _moveSpeed;

	protected Boolean IsMove { get { return Action == ShipAction.Move; } }
	protected Boolean IsStay { get { return Action == ShipAction.Stay; } }
	protected Boolean IsAttack { get { return Action == ShipAction.Attack; } }

	protected Boolean IsSelectable { get; set; }

	public Rect WindowGui { get; set; }

	public BaseShip()
	{
		IsSelectable = true;
		_moveSpeed = 4f;

		WindowGui = new Rect(5, 80, 150, 400);
		_endPosition = Vector3.zero;
	}
}
