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
	public Place CurrentLocation { get; set; }

	public void Awake()
	{
		_endPosition = Position;

		fsm = new ShipFSM();

		fsm.AddTransition(eShipState.Wait, eShipState.Wait, Wait);
		fsm.AddTransition(eShipState.Wait, eShipState.Explode, Explode);
		fsm.AddTransition(eShipState.Wait, eShipState.Select, Select);

		fsm.AddTransition(eShipState.Select, eShipState.Select, Select);
		fsm.AddTransition(eShipState.Select, eShipState.Wait, Wait);
		fsm.AddTransition(eShipState.Select, eShipState.Attack, Attack);
		fsm.AddTransition(eShipState.Select, eShipState.Move, Move);

		fsm.AddTransition(eShipState.Attack, eShipState.Attack, Attack);
		fsm.AddTransition(eShipState.Attack, eShipState.Rest, Rest);

		fsm.AddTransition(eShipState.Move, eShipState.Move, Move);
		fsm.AddTransition(eShipState.Move, eShipState.Rest, Rest);

		fsm.AddTransition(eShipState.Rest, eShipState.Rest, Rest);
		fsm.AddTransition(eShipState.Rest, eShipState.Wait, Wait);

		fsm.AddTransition(eShipState.Explode, eShipState.Explode, Explode);
	}

	public void Update() //loop
	{
		var nextPossibleState = GetState();
		State = fsm.Advance(nextPossibleState);
	}

	private eShipState GetState()
	{
		if (Health <= 0)
			return eShipState.Explode;

		if (_endPosition != Position)
			return eShipState.Move;

		if (_targetPosition != Vector3.zero)
			return eShipState.Attack;

		if (!IsSelectable)
			return eShipState.Rest;

		return IsSelected ? eShipState.Select : eShipState.Wait;
	}

	public void OnTriggerEnter(Collider other) // oneTime
	{
		if (other.tag == "Place" && State == eShipState.Move || State == eShipState.Wait)
		{
			_openCurrentLocation(other.gameObject);
		}
	}

	public void OnTriggerExit(Collider other) // oneTime
	{
		if (other.tag == "Place" && State == eShipState.Move)
		{
			_closeCurrentLocation(other.gameObject);
		}
	}

	public void OnMouseDown()
	{
		_toggleSelection();
	}

	#region Selection
	private void _toggleSelection()
	{
		if (IsSelectable) Current = IsSelected ? null : this; //ИСПРАВИТЬ - поставить условие if(PlayerWait)
	}

	public void Deselect()
	{
		if (IsSelectable) IsSelectable = false;
		if (IsSelected) Current = null;
	}
	#endregion

	private void _openCurrentLocation(GameObject placeObject)
	{
		CurrentLocation = placeObject.GetComponent<Place>();
		CurrentLocation.IsOpen = false;
	}

	private void _closeCurrentLocation(GameObject placeObject)
	{
		CurrentLocation = placeObject.GetComponent<Place>();
		CurrentLocation.IsOpen = true;
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
	protected void Rest()
	{

	}

	protected void Wait()
	{
		//if (CurrentType == eShipType.Small) InDebug("ship Wait");
	}

	protected void Select()
	{
	}

	protected void Explode()
	{
		Deselect();
	}

	protected void Attack()
	{
		Deselect();

		var bullet = Inst<Bullet>(PFactory.Bullet, this.Position, Quaternion.identity);
		bullet.SendTo(_targetPosition);

		//reset TARGET
		_targetPosition = Vector3.zero;
	}

	protected void Move()
	{
		if (IsSelectable)
		{
			//open once
			CurrentLocation.Open();
		}

		Deselect();

		Position = Vector3.Lerp(Position, _endPosition, Time.deltaTime * _moveSpeed);

		//reset PATH
		//if (Position == _endPosition) _endPosition = Vector3.zero;
	}
	#endregion
}

