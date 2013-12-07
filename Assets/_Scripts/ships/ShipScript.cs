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

	public PlayerScript Player { get { return PlayerScript.Current; } }

	public void Start()
	{
		_isSelected = false;
		_moveSpeed = 0.01f;

		WindowGui = new Rect(10, Screen.height - 50, 600, 150);
		_endPosition = Vector3.zero;
	}

	public void Update()
	{
		_moveShip();
	}

	public void OnMouseDown()
	{
		if (Struct.Action == ShipAction.Stay)
		{
			_isSelected = !_isSelected;

			if (_isSelected)
			{
				Current = this;
			}
			else
			{
				if (Current == null) return;

				PlayerScript.Current.Action = PlayerAction.Stay;
				Current = null;
			}
		}
	}

	public void OnGUI()
	{
		if (_isSelected)
		{
			WindowGui = GUI.Window(0, WindowGui, _createInventory, "Ships actions");
		}
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
			Position = Vector3.Lerp(Position, _endPosition, Time.time * _moveSpeed);

			if (Position == _endPosition)
			{
				_endPosition = Vector3.zero;
				Player.ResetAction();
			}
		}
	}

	private void _createInventory(int windowId)
	{
		GUILayout.BeginArea(new Rect(5, 20, WindowGui.width, 100));
		GUILayout.BeginHorizontal();

		if (GUILayout.Button("Move", GUILayout.Width(80), GUILayout.Height(30)))
		{
			print("move");
			PlayerScript.Current.Action = PlayerAction.Move;
		}

		if (GUILayout.Button("Attack", GUILayout.Width(80), GUILayout.Height(30)))
		{
			print("attack");
			PlayerScript.Current.Action = PlayerAction.Attack;
		}

		if (GUILayout.Button("Stay", GUILayout.Width(80), GUILayout.Height(30)))
		{
			print("stay");
			PlayerScript.Current.Action = PlayerAction.Stay;
		}

		GUILayout.Label("Health: " + Current.Struct.Health);
		GUILayout.Label("Power: " + Current.Struct.Power);
		GUILayout.Label("Action: " + Current.Struct.Action);

		GUILayout.EndHorizontal();
		GUILayout.EndArea();
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
	Middle,
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