using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;

public class Ship : BaseBehaviour<Ship>
{
	public Rect WindowGui { get; set; }

	private Vector3 _endPosition;
	private float _moveSpeed;
	public ShipStruct Struct { get; set; }

	private Boolean IsSelected { get { return Current == this; } }
	private Boolean IsSelectable { get; set; }

	public PlaceController PlaceController { get { return PlaceController.Current; } }
	
	protected Boolean IsMove { get { return Struct.Action == ShipAction.Move; } }
	protected Boolean IsStay { get { return Struct.Action == ShipAction.Stay; } }
	protected Boolean IsAttack { get { return Struct.Action == ShipAction.Attack; } }

	public void Start()
	{
		IsSelectable = true;
		_moveSpeed = 4f;

		WindowGui = new Rect(5, 80, 150, 400);
		_endPosition = Vector3.zero;
	}

	public void Update()
	{
		Move();
	}

	public void OnMouseDown()
	{
		if (IsStay)
		{
			Current = IsSelected ? null : this;

			if (!IsSelected)
			{
				DeselectShip();
				PlaceController.ResetSprites();
			}
		}
	}

	public void OnGUI()
	{
		var coorX = 120;
		var coorY = Screen.height - 100;
		var widthLabel = 150;
		var heightLabel = 20;

		if (IsSelected)
		{
			GUI.Label(new Rect(coorX, coorY, widthLabel, heightLabel), "*" + Struct.Type + "Ship*");
			GUI.Label(new Rect(coorX, coorY + 20, widthLabel, heightLabel), "Health: " + Struct.Health);
			GUI.Label(new Rect(coorX, coorY + 40, widthLabel, heightLabel), "Power: " + Struct.Power);
			GUI.Label(new Rect(coorX, coorY + 60, widthLabel, heightLabel), "Action: " + Struct.Action);
		}
	}

	public void DeselectShip()
	{
		Player.SetAction(PlayerAction.Wait); //todo: ??
		Current = null;
	}

	public void SetDestination(Vector3 destinationPosition)
	{
		_endPosition = destinationPosition;
		DeselectShip();
	}

	public void SetPosition(Vector3 startPosition)
	{
		this.gameObject.SetActive(true);
		transform.position = startPosition;
	}

	#region Actions
	public void Attack(Vector3 targetPosition)
	{
		var bullet = Inst<Bullet>(PFactory.Bullet, Position, Quaternion.identity);
		bullet.EndPosition = targetPosition;
	}

	protected void Move()
	{
		if (_endPosition != Vector3.zero)
		{
			Struct.Action = ShipAction.Move;

			Position = Vector3.Lerp(Position, _endPosition, Time.deltaTime * _moveSpeed);

			if (Position == _endPosition)
			{
				_endPosition = Vector3.zero;
				Struct.Action = ShipAction.Stay;

				Player.ResetAction();
			}
		}
	}
	#endregion
}

public class ShipStruct
{
	public int Id { get; set; }

	public ShipType Type { get; set; }
	public ShipAction Action { get; set; }
	public ShipState State { get; set; }

	public int Health;
	public int Power { get; set; }

	public void Start()
	{
		Id = 0;

		Type = ShipType.Small;
		Action = ShipAction.Stay;
		State = ShipState.Alive;

		Power = 0;
		Health = 0;
	}
}

public enum ShipType
{
	Small,
	Medium,
	Big
}

public enum ShipAction
{
	Move,
	Stay,
	Attack
}

public enum ShipState
{
	Alive,
	Wounded,
	Dead
}