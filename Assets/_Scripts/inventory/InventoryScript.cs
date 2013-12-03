using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class InventoryScript : BaseBehaviour<InventoryScript>
{
	#region MainWindow
	public float startX;
	public float startY;

	public float width;
	public float height;

	private bool _inventoryToggle;
	private Rect _window;
	#endregion

	public float BtnWidth = 50;
	public float BtnHeight = 50;

	#region Ship Icons
	public Texture btnTexSmallShip;
	public Texture btnTexMiddleShip;
	public Texture btnTexBigShip;
	#endregion

	private List<InventoryItem> _items;

	private int toolbarId;

	public void Start()
	{
		_items = new List<InventoryItem>();
		TestItems();

		_inventoryToggle = true;

		_window = new Rect(startX - 10, startY + 20, width, height);

		toolbarId = 0;
	}

	public void OnGUI()
	{
		var textures = new Texture[2];
		textures[0] = btnTexSmallShip;
		textures[1] = btnTexMiddleShip;

		toolbarId = GUI.Toolbar(new Rect(5, 400, 200, 200), toolbarId, textures);

		_inventoryToggle = GUI.Toggle(new Rect(startX, startY, 100, 20), _inventoryToggle, "Inventory");

		if (_inventoryToggle)
		{
			_window = GUI.Window(0, _window, CreateInventory, String.Empty);
		}
	}

	public void CreateInventory(int windowId)
	{
		GUILayout.BeginArea(new Rect(_window.x, _window.y, _window.width, _window.height));
		GUILayout.BeginVertical();

		foreach (var item in _items)
		{
			if (GUILayout.Button(item.Image, GUILayout.Width(BtnWidth), GUILayout.Height(BtnHeight)))
			{
				item.IsOn = true;
			}
			else
			{
				item.IsOn = false;
			}
		}

		GUILayout.EndVertical();
		GUILayout.EndArea();
	}

	private void TestItems()
	{
		var item1 = new InventoryItem()
		{
			Id = 1,
			Name = "Small-Spider",
			Description = "Small ship",
			Image = btnTexSmallShip
		};

		var item2 = new InventoryItem()
		{
			Id = 2,
			Name = "Middle-Aircraft",
			Description = "Middle ship",
			Image = btnTexMiddleShip
		};

		var item3 = new InventoryItem()
		{
			Id = 3,
			Name = "Big-Horn",
			Description = "Big ship",
			Image = btnTexBigShip
		};

		_items.Add(item1);
		_items.Add(item2);
		_items.Add(item3);
	}
}
