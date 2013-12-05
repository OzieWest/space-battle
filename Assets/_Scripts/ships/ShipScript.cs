using System;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;

public class ShipScript : BaseBehaviour<ShipScript>
{
	#region Window
	public float WindowW = 600f;
	public float WindowH = 60f;
	private Rect _window_gui;
	#endregion

	private Boolean _isSelected = false;

	public ShipStruct S
	{
		get { return gameObject.GetComponent<ShipStruct>(); }
	}

	public void Start()
	{
		Put(
			"SelectionRing",
			Inst(PrefabFactory.Current.SelectionRing, gameObject.transform.position, transform.rotation)
		);

		_window_gui = new Rect(10, Screen.height - 50, WindowW, WindowH);
	}

	public void Update()
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

	public void OnMouseDown()
	{
		_isSelected = !_isSelected;

		if (_isSelected)
		{
			Current = this;
		}
		else
		{
			if (Current != null)
			{
				PlayerScript.Current.Action = PlayerAction.Undefine;
				Current = null;
			}
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
			_window_gui = GUI.Window(0, _window_gui, CreateInventory, "Actions");
		}
	}

	public void CreateInventory(int windowId)
	{
		GUILayout.BeginArea(new Rect(5, 20, _window_gui.width, 100));
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
