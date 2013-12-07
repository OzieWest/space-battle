using System;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;

public class ShipScript : BaseBehaviour<ShipScript>
{
	private Boolean _isSelected;
	private Vector3 _endPosition;
	private float _moveSpeed;

	public Rect WindowGui { get; set; }
	public ShipStruct Struct { get; set; }

	public Boolean isActionDo;

	public PlayerScript Player { get { return PlayerScript.Current; } }

	public void Start()
	{
		_isSelected = false;
		_moveSpeed = 0.01f;

		WindowGui = new Rect(5, 80, 150, 400);
		_endPosition = Vector3.zero;

		isActionDo = false;
	}

	public void Update()
	{
		_moveShip();
	}

	public void OnMouseDown()
	{
		if (Struct.Action == ShipAction.Stay)
		{
			if (Current == this)
			{
				Debug.Log("Z");
			}

			_isSelected = !_isSelected;

			if (_isSelected)
			{
				Current = this;
			}
			else
			{
				if (Current == null) return;

				DeselectShip();
			}
		}
	}

	public void OnGUI()
	{
		if (_isSelected)
		{
			GUI.Label(new Rect(5, 80, 150, 20), "*" + Current.Struct.Type.ToString() + "Ship*");
			GUI.Label(new Rect(5, 100, 150, 20), "Health: " + Current.Struct.Health);
			GUI.Label(new Rect(5, 120, 150, 20), "Power: " + Current.Struct.Power);
			GUI.Label(new Rect(5, 140, 150, 20), "Action: " + Current.Struct.Action);
		}
	}

	private void DeselectShip()
	{
		Player.SetAction(PlayerAction.Wait);
		Current = null;
	}

	public void SetDestination(Vector3 destination)
	{
		Current._endPosition = destination;
	}

	public Boolean IsShipMove()
	{
		return Struct.Action == ShipAction.Move;
	}

	public Boolean IsShipStay()
	{
		return Struct.Action == ShipAction.Stay;
	}

	public Boolean IsShipAttack()
	{
		return Struct.Action == ShipAction.Attack;
	}

	private void _moveShip()
	{
		if (_endPosition != Vector3.zero)
		{
			Struct.Action = ShipAction.Move;

			Position = Vector3.Lerp(Position, _endPosition, Time.time * _moveSpeed);

			if (Position == _endPosition)
			{
				_endPosition = Vector3.zero;
				Player.ResetAction();
				
				Struct.Action = ShipAction.Stay;

				isActionDo = true;
			}
		}
	}
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

		Type = ShipType.Unknown;
		Action = ShipAction.Unknown;
		State = ShipState.Unknown;

		Power = 0;
		Health = 0;
	}
}

public enum ShipType
{
	Unknown = 0,
	Small,
	Medium,
	Big
}

public enum ShipAction
{
	Unknown = 0,
	Move,
	Stay,
	Attack
}

public enum ShipState
{
	Unknown = 0,
	Alive,
	Wounded,
	Dead
}