using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;

public class Ship : BaseShip<Ship>
{
	private Boolean IsSelected { get { return Current == this; } }

	public PlaceController PlaceController { get { return PlaceController.Current; } }
	public Place Location { get; set; }

	public void Start()
	{
		
	}

	public void Update() //loop
	{
		Move();
		Attack();
	}

	public void OnTriggerEnter(Collider other) // oneTime
	{
		if (other.tag == "Place")
		{
			SetLocation(other.gameObject);
		}
	}

	public void OnMouseDown() //loop
	{
		if (IsStay && IsSelectable)
		{
			Current = IsSelected ? null : this;

			if (!IsSelected)
			{
				Deselect();
				PlaceController.SetDefaultSprites();
			}
		}
	}

	public void OnGUI() //loop
	{
		if (IsSelected)
		{
			var coor = PlaceController.GetCoordinateLastRow();
			coor.y *= -1;
			
			var screenCoor = Camera.main.WorldToScreenPoint(coor);
			screenCoor.x -= 20;
			screenCoor.y += 25;

			var widthLabel = 150;
			var heightLabel = 20;

			GUI.Label(new Rect(screenCoor.x, screenCoor.y, widthLabel, heightLabel), "*" + Type + "Ship*");
			GUI.Label(new Rect(screenCoor.x, screenCoor.y + 20, widthLabel, heightLabel), "Health: " + Health);
			GUI.Label(new Rect(screenCoor.x + 60, screenCoor.y + 20, widthLabel, heightLabel), "Power: " + Power);
			GUI.Label(new Rect(screenCoor.x, screenCoor.y + 40, widthLabel, heightLabel), "Action: " + Action);
		}
	}

	public void Deselect()
	{
		Current = null;
	}

	public void SetLocation(GameObject placeObject)
	{
		Location = placeObject.GetComponent<Place>();
		Location.Close();
	}

	public void SetTarget(Vector3 targetPosition)
	{
		_targetPosition = targetPosition;
	}

	public void SetDestination(Vector3 destinationPosition)
	{
		_endPosition = destinationPosition;
	}

	public void SetPosition(Vector3 startPosition)
	{
		this.gameObject.SetActive(true);
		transform.position = startPosition;
	}

	#region Actions
	protected void Attack()
	{
		if (_targetPosition != Vector3.zero)
		{
			var bullet = Inst<Bullet>(PFactory.Bullet, this.Position, Quaternion.identity);
			bullet.SendTo(_targetPosition);

			_targetPosition = Vector3.zero;
			IsSelectable = false;

			Deselect();
		}
	}

	protected void Move()
	{
		if (_endPosition != Vector3.zero)
		{
			if (!IsMove)
			{
				Location.Open();
				Deselect();
				Action = ShipAction.Move;
			}

			Position = Vector3.Lerp(Position, _endPosition, Time.deltaTime * _moveSpeed);

			if (Position == _endPosition)
			{
				IsSelectable = false;
				_endPosition = Vector3.zero;
				Action = ShipAction.Stay;
				Player.ResetAction();
			}
		}
	}

	protected void Rotate()
	{
		
	}
	#endregion
}

public enum ShipType
{
	Small,
	Medium,
	Big
}

public enum ShipAction
{
	Move,
	Stay,
	Attack
}

public enum ShipState
{
	Alive,
	Wounded,
	Dead
}