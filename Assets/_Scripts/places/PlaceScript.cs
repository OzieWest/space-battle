using System;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;

public class PlaceScript : BaseBehaviour<PlaceScript>
{
	public static Color DefaultColor;
	public static Color MouseOverColor;
	public static Color MouseDownColor;

	public ShipScript CurrentShip { get { return ShipScript.Current; } }
	public PlayerScript Player { get { return PlayerScript.Current; } }

	static PlaceScript()
	{
		DefaultColor = Color.white;
		MouseOverColor = Color.black;
	}

	public void OnMouseDown()
	{
		if (Player.IsActionMove())
		{
			CurrentShip.SetDestination(
				Player.IconMove.transform.position
			);
		}
	}

	public void OnMouseOver()
	{
		if (Player.IsActionMove())
		{
			SetPosition(
				Player.IconMove,
				Position
			);

			if (!Player.IconMove.active)
				Player.IconMove.SetActive(true);
		}

		if (_isShipSelect())
		{
			SetColor(MouseOverColor);
		}
	}

	public void OnMouseExit()
	{
		if (_isShipSelect())
		{
			SetColor(DefaultColor);
		}

		if (Player.IconMove.active)
		{
			Player.IconMove.SetActive(false);
		}
	}

	private Boolean _isShipSelect()
	{
		return CurrentShip != null;
	}
}
