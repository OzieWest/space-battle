using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class InventoryController : MonoBehaviour
{
	public float startX;
	public float startY;

	public float width;
	public float height;

	private bool _inventoryToggle;
	private Rect _window;

	private List<InventoryItem> _items;

	public void Start()
	{
		_items = new List<InventoryItem>();
		TestItems();

		_inventoryToggle = true;

		_window = new Rect(startX - 10, startY + 20, width, height);
	}

	public void OnGUI()
	{
		_inventoryToggle = GUI.Toggle(new Rect(startX, startY, 100, 20), _inventoryToggle, "Inventory");

		if (_inventoryToggle)
		{
			_window = GUI.Window(0, _window, CreateInventory, String.Empty);
		}
	}

	public void CreateInventory(int windowId)
	{
		GUILayout.BeginArea(new Rect(_window.x + 5, _window.y + 10, _window.width, _window.height));

		GUILayout.BeginVertical();

		foreach (var item in _items)
			GUILayout.Button(item.Name, GUILayout.Height(30));	
		
		GUILayout.EndVertical();

		GUILayout.EndArea();
	}

	private void TestItems()
	{
		var item1 = new InventoryItem()
		{
			Id = 1,
			Name = "Ship 1",
			Description = "First ship"
		};

		var item2 = new InventoryItem()
		{
			Id = 2,
			Name = "Ship 2",
			Description = "Second ship"
		};

		var item3 = new InventoryItem()
		{
			Id = 3,
			Name = "Ship 3",
			Description = "Third ship"
		};

		_items.Add(item1);
		_items.Add(item2);
		_items.Add(item3);
	}
}
