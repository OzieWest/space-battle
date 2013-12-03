using System;
using UnityEngine;
using System.Collections;

public class PlaceScript : BaseBehaviour<PlaceScript>
{
	public void OnMouseOver()
	{
		if (IsShipSelect())
			SetColor(Color.black);
	}

	public void OnMouseExit()
	{
		if (IsShipSelect())
			SetColor(Color.white);
	}

	public Boolean IsShipSelect()
	{
		return ShipScript.Current != null;
	}
}
