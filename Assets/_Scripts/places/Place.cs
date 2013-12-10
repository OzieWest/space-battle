using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;

public class Place : BaseBehaviour<Place>
{
	public Boolean IsFree { get; set; }

	public Place Top { get; set; }
	public Place Bottom { get; set; }
	public Place Left { get; set; }
	public Place Right { get; set; }

	public Ship CurrentShip { get { return Ship.Current; } }
	public Player Player { get { return Player.Current; } }
	public GridController Grid { get { return GridController.Current; } }

	public void Start()
	{
		IsFree = true;
	}

	public void OnMouseDown()
	{
		if (GetSprite() == Grid.IconMove)
		{
			CurrentShip.SetDestination(this);
		}
		else if (GetSprite() == Grid.IconAttack)
		{
			CurrentShip.Attack(this);
		}
	}
	
	public void SetSprite(Sprite sprite, Color color)
	{
		var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = sprite;
		spriteRenderer.color = color;
	}

	public Sprite GetSprite()
	{
		var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		return spriteRenderer.sprite;
	}

	private Boolean _isShipSelect()
	{
		return CurrentShip != null;
	}

	public List<Place> GetNeighbors()
	{
		var result = new List<Place>();
		
		if (Top != null)
			result.Add(Top);

		if (Bottom != null)
			result.Add(Bottom);

		if (Left != null)
			result.Add(Left);

		if (Right != null)
			result.Add(Right);

		return result;
	}

	public void Free()
	{
		IsFree = true;
	}

	public void NotFree()
	{
		IsFree = false;
	}
}
