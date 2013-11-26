using System;
using UnityEngine;
using System.Collections;

public class InventoryItem
{
	public int Id { get; set; }
	public String  Name { get; set; }
	public Texture2D Image { get; set; }
	public String Description { get; set; }

	public InventoryItem()
	{
		Id = 0;
		Name = String.Empty;
		Image = null;
		Description = String.Empty;
	}
}
