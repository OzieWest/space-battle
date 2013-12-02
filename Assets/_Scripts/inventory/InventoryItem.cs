using System;
using UnityEngine;
using System.Collections;

public class InventoryItem
{
	public int Id { get; set; }
	public String  Name { get; set; }
	public Texture Image { get; set; }
	public String Description { get; set; }

	public Boolean IsActive { get; set; }
	public Boolean IsOn { get; set; }

	public InventoryItem()
	{
		Id = 0;
		Name = String.Empty;
		Image = null;
		Description = String.Empty;
		IsActive = false;
		IsOn = false;
	}
}
