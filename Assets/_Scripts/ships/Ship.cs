using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using System.Collections;

public class ShipFSM : FiniteStateMachine<eShipState> { };

public class Ship : BaseShip<Ship>
{
	private ShipFSM fsm;

	private Boolean IsSelected { get { return Current == this; } }

	public PlaceController PlaceController { get { return PlaceController.Current; } }
	public Place Location { get; set; }

	public void Start()
	{
		fsm = new ShipFSM();

		fsm.AddTransition(eShipState.Stay, eShipState.Stay, Stay);
		fsm.AddTransition(eShipState.Stay, eShipState.Explode, Explode);
		fsm.AddTransition(eShipState.Stay, eShipState.Selected, Select);

		fsm.AddTransition(eShipState.Selected, eShipState.Selected, Select);
		fsm.AddTransition(eShipState.Selected, eShipState.Stay, Stay);
		fsm.AddTransition(eShipState.Selected, eShipState.Attack, Attack);
		fsm.AddTransition(eShipState.Selected, eShipState.Move, Move);

		fsm.AddTransition(eShipState.Attack, eShipState.Attack, Attack);
		fsm.AddTransition(eShipState.Attack, eShipState.Stay, Stay);

		fsm.AddTransition(eShipState.Move, eShipState.Move, Move);
		fsm.AddTransition(eShipState.Move, eShipState.Stay, Stay);
	}

	public void Update() //loop
	{
		if(IsSelected)
			UnityEngine.Debug.Log(IsSelectable);

		CurrentState = GetState();
		fsm.Advance(CurrentState);
	}

	private eShipState GetState()
	{
		if (Health <= 0)
			return eShipState.Explode;

		//if (!IsSelectable)
		//	return eShipState.Stay;

		if (IsSelectable && _endPosition != Vector3.zero)
			return eShipState.Move;

		if (IsSelectable && _targetPosition != Vector3.zero)
			return eShipState.Attack;

		if (!IsSelected || !IsSelectable)
			return eShipState.Stay;
		else
			return eShipState.Selected;
	}

	public void OnTriggerEnter(Collider other) // oneTime
	{
		if (other.tag == "Place") SetLocation(other.gameObject);
	}

	public void OnMouseDown()
	{
		if (IsSelectable) Current = IsSelected ? null : this;
	}

	public void OnGUI() //loop
	{
		//if (CurrentState == eShipState.Selected)
		//{
		//	var coor = PlaceController.GetCoordinateLastRow();
		//	coor.y *= -1;

		//	var screenCoor = Camera.main.WorldToScreenPoint(coor);
		//	screenCoor.x -= 20;
		//	screenCoor.y += 25;

		//	var widthLabel = 150;
		//	var heightLabel = 20;

		//	GUI.Label(new Rect(screenCoor.x, screenCoor.y, widthLabel, heightLabel), "*" + Type + "Ship*");
		//	GUI.Label(new Rect(screenCoor.x, screenCoor.y + 20, widthLabel, heightLabel), "Health: " + Health);
		//	GUI.Label(new Rect(screenCoor.x + 60, screenCoor.y + 20, widthLabel, heightLabel), "Power: " + Power);
		//}
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

	#region Actions
	protected void Stay()
	{
		if (IsSelected) GameSettings.Log(MethodBase.GetCurrentMethod().Name);
	}

	protected void Select()
	{
		if (IsSelected) GameSettings.Log(MethodBase.GetCurrentMethod().Name);
	}

	protected void Explode()
	{
		IsSelectable = false;
		if (IsSelected) GameSettings.Log(MethodBase.GetCurrentMethod().Name);
	}

	protected void Attack()
	{
		if (IsSelected) GameSettings.Log(MethodBase.GetCurrentMethod().Name);

		IsSelectable = false;

		//if (_targetPosition != Vector3.zero)
		//{
		//	var bullet = Inst<Bullet>(PFactory.Bullet, this.Position, Quaternion.identity);
		//	bullet.SendTo(_targetPosition);

		//	_targetPosition = Vector3.zero;
		//	IsSelectable = false;

		//	Deselect();
		//}
	}

	protected void Move()
	{
		if (IsSelected) GameSettings.Log(MethodBase.GetCurrentMethod().Name);

		Position = Vector3.Lerp(Position, _endPosition, Time.deltaTime * _moveSpeed);

		if (Position == _endPosition)
		{
			IsSelectable = false;
			_endPosition = Vector3.zero;
		}

		//if (_endPosition != Vector3.zero)
		//{
		//	if (!IsMove)
		//	{
		//		Location.Open();
		//		Deselect();
		//		Action = ShipAction.Move;
		//	}

		//	if (Position == _endPosition)
		//	{
		//		IsSelectable = false;
		//		_endPosition = Vector3.zero;
		//		Action = ShipAction.Stay;
		//		Player.ResetAction();
		//	}
		//}
	}
	#endregion
}

