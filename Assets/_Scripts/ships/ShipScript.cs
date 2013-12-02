using System;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;

public class ShipScript : MonoBehaviour
{
	public float width;
	public float height;
	private Rect _window;

	public GameObject PF_Bullet;

	public static GameObject CurrentShip;
	public GameObject PF_SelectionRing;
	private GameObject INS_selectionRing;

	private ShipType _shipType;
	private Boolean _isSelected;

	private ShipAction _currentAction;

	public void Start()
	{
		INS_selectionRing = (GameObject)Instantiate(PF_SelectionRing, gameObject.transform.position, transform.rotation);
		HideSelect();
		_isSelected = false;

		_window = new Rect(10, 100, width, height);

		_currentAction = ShipAction.Unknown;
	}

	public void Update()
	{
		if (_isSelected)
		{
			ShowSelect();
		}
		else
		{
			HideSelect();
		}
	}

	public void OnMouseDown()
	{
		_isSelected = !_isSelected;

		CurrentShip = _isSelected ? gameObject : null;
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
			_window = GUI.Window(0, _window, CreateInventory, String.Empty);
		}
	}

	public void CreateInventory(int windowId)
	{
		GUILayout.BeginArea(new Rect(_window.x, _window.y, _window.width, _window.height));
		GUILayout.BeginVertical();

		if (GUILayout.Button("Move", GUILayout.Width(80), GUILayout.Height(30)))
		{
			print("move");
		}

		if (GUILayout.Button("Attack", GUILayout.Width(80), GUILayout.Height(30)))
		{
			print("attack");
		}

		if (GUILayout.Button("Stay", GUILayout.Width(80), GUILayout.Height(30)))
		{
			print("stay");
		}

		GUILayout.EndVertical();
		GUILayout.EndArea();
	}

	protected void ShowSelect()
	{
		INS_selectionRing.active = true;
	}

	protected void HideSelect()
	{
		INS_selectionRing.active = false;
	}
}
