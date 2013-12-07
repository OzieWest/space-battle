using System;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;

public class ShipScript : BaseBehaviour<ShipScript>
{
	public Rect WindowGui;

	private Boolean _isSelected = false;

	public ShipStruct S { get; set; }

	public void Start()
	{
		Put(
			"SelectionRing",
			Inst(PrefabFactory.Current.SelectionRing, gameObject.transform.position, transform.rotation)
		);

		WindowGui = new Rect(10, Screen.height - 50, 600, 150);
	}

	public void Update()
	{
		_toggleSelectionIcon();
	}

	public void OnMouseDown()
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

	public void OnMouseOver()
	{
	}

	public void OnMouseExit()
	{
	}

	public void OnGUI()
	{
		if (_isSelected)
		{
			WindowGui = GUI.Window(0, WindowGui, CreateInventory, "Actions");
		}
	}

	private void _toggleSelectionIcon()
	{
		if (_isSelected)
		{
			TurnOn("SelectionRing");
		}
		else
		{
			TurnOff("SelectionRing");
		}
	}

	private void CreateInventory(int windowId)
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

		GUILayout.Label("Health: " + Current.S.Health);
		GUILayout.Label("Power: " + Current.S.Power);

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
	Fire
}

public enum ShipState
{
	Unknown = 0,
	Alive,
	Wounded,
	Dead
}