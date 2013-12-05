using System;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;

public class PlaceScript : BaseBehaviour<PlaceScript>
{
	public static Color DefaultColor;
	public static Color MouseOverColor;
	public static Color MouseDownColor;

	static PlaceScript()
	{
		DefaultColor = Color.white;
		MouseOverColor = Color.black;
	}

	public void OnMouseDown()
	{
		if (PlayerScript.Current.Action == PlayerAction.Move)
		{
			
		}
	}

	public void OnMouseOver()
	{
		if (PlayerScript.Current.Action == PlayerAction.Move)
		{
			SetPosition(
				PlayerScript.Current.IconMove,
				Position
			);

			if (!PlayerScript.Current.IconMove.active)
				PlayerScript.Current.IconMove.SetActive(true);
		}

		if (IsShipSelect())
		{
			SetColor(MouseOverColor);
		}
	}

	public void OnMouseExit()
	{
		if (IsShipSelect())
		{
			SetColor(DefaultColor);
		}

		if (PlayerScript.Current.IconMove.active)
			PlayerScript.Current.IconMove.SetActive(false);
	}

	public Boolean IsShipSelect()
	{
		return ShipScript.Current != null;
	}

	public Boolean IsShipMove()
	{
		return ShipScript.Current.S.Action == ShipAction.Move;
	}
}
