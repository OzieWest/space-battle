using System;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;

public class ShipScript : BaseBehaviour<ShipScript>
{
	#region Window
	public float windowWidth_gui = 150f;
	public float windowHeight_gui = 300f;
	private Rect _window_gui;
	#endregion

	private Boolean _isSelected;
	public ShipAction Action;

	public void Start()
	{
		Put(
			"SelectionRing",
			Inst(PrefabFactory.Current.SelectionRing, gameObject.transform.position, transform.rotation)
		);

		HideSelectRing();

		_isSelected = false;

		_window_gui = new Rect(10, 100, windowWidth_gui, windowHeight_gui);
	}

	public void Update()
	{
		if (_isSelected)
		{
			ShowSelectRing();
		}
		else
		{
			HideSelectRing();
		}
	}

	public void OnMouseDown()
	{
		_isSelected = !_isSelected;

		Current = _isSelected ? this : null;
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
			_window_gui = GUI.Window(0, _window_gui, CreateInventory, String.Empty);
		}
	}

	public void CreateInventory(int windowId)
	{
		GUILayout.BeginArea(new Rect(_window_gui.x, _window_gui.y, _window_gui.width, _window_gui.height));
		GUILayout.BeginVertical();

		if (GUILayout.Button("Move", GUILayout.Width(80), GUILayout.Height(30)))
		{
			print("move");
			Action = ShipAction.Move;
		}

		if (GUILayout.Button("Attack", GUILayout.Width(80), GUILayout.Height(30)))
		{
			print("attack");
			Action = ShipAction.Fire;
		}

		if (GUILayout.Button("Stay", GUILayout.Width(80), GUILayout.Height(30)))
		{
			print("stay");
			Action = ShipAction.Stay;
		}

		GUILayout.EndVertical();
		GUILayout.EndArea();
	}

	protected void ShowSelectRing()
	{
		TurnOn("SelectionRing");
	}

	protected void HideSelectRing()
	{
		TurnOff("SelectionRing");
	}
}
