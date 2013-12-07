using System;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;

public class PlaceScript : BaseBehaviour<PlaceScript>
{
	public static Color DefaultColor;
	public static Color MouseOverColor;
	public static Color MouseDownColor;

	public Boolean IsFree { get; set; }

	public ShipScript CurrentShip { get { return ShipScript.Current; } }
	public PlayerScript Player { get { return PlayerScript.Current; } }

	public void Start()
	{
		IsFree = true;
	}

	static PlaceScript()
	{
		DefaultColor = Color.white;
		MouseOverColor = Color.black;
	}

	public void OnMouseDown()
	{

	}

	public void OnMouseOver()
	{
		
	}

	public void OnMouseExit()
	{
		
	}

	private Boolean _isShipSelect()
	{
		return CurrentShip != null;
	}
}
