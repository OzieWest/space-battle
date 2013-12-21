using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;

public enum ePlaceState
{
	Open,
	Close,
	Attack,
	Move,
	Destroyed
}

public class PlaceFSM : FiniteStateMachine<ePlaceState> { }

public class Place : BaseBehaviour<Place>
{
	public int[] Index { get; set; } //Для диагностики

	private PlaceFSM fsm;
	public String FsmSymbol { get; set; }

	public Boolean IsOpen { get; set; }
	public Boolean IsNeighbor { get; set; }
	public Boolean IsAttack { get; set; }
	public Boolean IsChecked { get; set; }

	public ePlaceState State { get; set; }

	public Place Top { get; set; }
	public Place Bottom { get; set; }
	public Place Left { get; set; }
	public Place Right { get; set; }

	public Ship CurrentShip { get { return Ship.Current; } }
	public SpriteRenderer SpriteRenderer { get; private set; }

	public void Awake()
	{
		Index = new int[2];

		IsOpen = true;
		IsNeighbor = false;
		IsAttack = false;
		IsChecked = false;
		State = ePlaceState.Open;

		SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

		fsm = new PlaceFSM();

		fsm.AddTransition(ePlaceState.Open, ePlaceState.Open, ActionOpen);
		fsm.AddTransition(ePlaceState.Open, ePlaceState.Move, ActionMove);
		fsm.AddTransition(ePlaceState.Open, ePlaceState.Attack, ActionAttack);
		fsm.AddTransition(ePlaceState.Open, ePlaceState.Close, ActionClose);

		fsm.AddTransition(ePlaceState.Close, ePlaceState.Close, ActionClose);
		fsm.AddTransition(ePlaceState.Close, ePlaceState.Open, ActionOpen);

		fsm.AddTransition(ePlaceState.Move, ePlaceState.Move, ActionMove);
		fsm.AddTransition(ePlaceState.Move, ePlaceState.Open, ActionOpen);

		fsm.AddTransition(ePlaceState.Attack, ePlaceState.Attack, ActionAttack);
		fsm.AddTransition(ePlaceState.Attack, ePlaceState.Open, ActionOpen);
		fsm.AddTransition(ePlaceState.Attack, ePlaceState.Destroyed, ActionDestroyed);

		fsm.AddTransition(ePlaceState.Destroyed, ePlaceState.Destroyed, ActionDestroyed);
	}

	public void Update()
	{
		var nextPossibleState = GetPossibleState();
		State = fsm.Advance(nextPossibleState);
	}

	#region Events
	public void OnMouseDown()
	{
		if (CurrentShip != null && CurrentShip.State == eShipState.Select)
		{
			if (State == ePlaceState.Move)
			{
				IsChecked = true;
				CurrentShip.SetDestination(this.Position);
			}
			else if (State == ePlaceState.Attack)
			{
				IsChecked = true;
				CurrentShip.SetTarget(this.Position);
			}
		}
	}
	#endregion

	#region Sprite
	public void SetSprite(Sprite sprite)
	{
		SpriteRenderer.sprite = sprite;
	}

	public Sprite GetSprite()
	{
		return SpriteRenderer.sprite;
	}
	#endregion

	#region Actions
	protected ePlaceState GetPossibleState()
	{
		if (IsChecked)
			return ePlaceState.Destroyed;

		if (IsAttack)
			return ePlaceState.Attack;

		if (IsNeighbor)
			return ePlaceState.Move;

		if (IsOpen)
			return ePlaceState.Open;
		
		return ePlaceState.Close;
	}

	public void ActionDestroyed()
	{
		if (SpriteRenderer.sprite != IFactory.PlaceWait)
			SpriteRenderer.sprite = IFactory.PlaceWait;

		IsChecked = true;
		IsAttack = false;
		IsNeighbor = false;
	}

	public void ActionMove()
	{
		if (SpriteRenderer.sprite != IFactory.PlaceMove)
			SpriteRenderer.sprite = IFactory.PlaceMove;
	}

	public void ActionAttack()
	{
		if (SpriteRenderer.sprite != IFactory.PlaceAttack)
			SpriteRenderer.sprite = IFactory.PlaceAttack;
	}

	public void ActionOpen()
	{
		if (SpriteRenderer.sprite != IFactory.PlaceOpen)
			SpriteRenderer.sprite = IFactory.PlaceOpen;
	}

	public void ActionClose()
	{
		if (SpriteRenderer.sprite != IFactory.PlaceClose)
			SpriteRenderer.sprite = IFactory.PlaceClose;
	}

	public void ActionDefault()
	{
		IsAttack = false;
		IsNeighbor = false;
	}
	#endregion

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
}
