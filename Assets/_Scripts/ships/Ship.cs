using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;

public class Ship : BaseBehaviour<Ship>
{
	public Rect WindowGui { get; set; }

	private Vector3 _targetPosition;
	private Vector3 _endPosition;
	private float _moveSpeed;
	public ShipStruct Struct { get; set; }

	private Boolean IsSelected { get { return Current == this; } }
	private Boolean IsSelectable { get; set; }

	public PlaceController PlaceController { get { return PlaceController.Current; } }
	public Place Location { get; set; }

	protected Boolean IsMove { get { return Struct.Action == ShipAction.Move; } }
	protected Boolean IsStay { get { return Struct.Action == ShipAction.Stay; } }
	protected Boolean IsAttack { get { return Struct.Action == ShipAction.Attack; } }

	public void Start()
	{
		IsSelectable = true;
		_moveSpeed = 4f;

		WindowGui = new Rect(5, 80, 150, 400);
		_endPosition = Vector3.zero;
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
			print("ship: OnTriggerEnter");
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
				PlaceController.ResetSprites();
			}
		}
	}

	public void OnGUI()
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

			GUI.Label(new Rect(screenCoor.x, screenCoor.y, widthLabel, heightLabel), "*" + Struct.Type + "Ship*");
			GUI.Label(new Rect(screenCoor.x, screenCoor.y + 20, widthLabel, heightLabel), "Health: " + Struct.Health);
			GUI.Label(new Rect(screenCoor.x + 60, screenCoor.y + 20, widthLabel, heightLabel), "Power: " + Struct.Power);
			GUI.Label(new Rect(screenCoor.x, screenCoor.y + 40, widthLabel, heightLabel), "Action: " + Struct.Action);
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
			var startPosition = Position;

			var bullet = Inst<Bullet>(PFactory.Bullet, startPosition, Quaternion.identity);
			bullet.EndPosition = _targetPosition;

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
				Struct.Action = ShipAction.Move;
			}

			Position = Vector3.Lerp(Position, _endPosition, Time.deltaTime * _moveSpeed);

			if (Position == _endPosition)
			{
				IsSelectable = false;
				_endPosition = Vector3.zero;
				Struct.Action = ShipAction.Stay;
				Player.ResetAction();
			}
		}
	}

	protected void Rotate()
	{
		
	}
	#endregion
}

public class ShipStruct
{
	public int Id { get; set; }

	public ShipType Type { get; set; }
	public ShipAction Action { get; set; }
	public ShipState State { get; set; }

	public int Health;
	public int Power { get; set; }

	public void Start()
	{
		Id = 0;

		Type = ShipType.Small;
		Action = ShipAction.Stay;
		State = ShipState.Alive;

		Power = 0;
		Health = 0;
	}
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