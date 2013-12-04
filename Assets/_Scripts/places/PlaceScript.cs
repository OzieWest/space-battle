using System;
using UnityEngine;
using System.Collections;

public class PlaceScript : BaseBehaviour<PlaceScript>
{
	public void OnMouseOver()
	{
		if (ShipScript.Current.Action == ShipAction.Move)
		{
			SetPosition(
				PlayerScript.Current.IconMove,
				Position
			);
			PlayerScript.Current.IconMove.SetActive(true);
		}

		if (IsShipSelect())
		{
			SetColor(Color.black);
		}
	}

	public void OnMouseExit()
	{
		if (IsShipSelect())
		{
			SetColor(Color.white);
		}

		PlayerScript.Current.IconMove.SetActive(false);
	}

	public Boolean IsShipSelect()
	{
		return ShipScript.Current != null;
	}
}
