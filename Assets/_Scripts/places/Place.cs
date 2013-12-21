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
	Wait
}

public class PlaceFSM : FiniteStateMachine<ePlaceState> { }

public class Place : BaseBehaviour<Place>
{
	private PlaceFSM fsm;
	public String FsmSymbol { get; set; }
	
	public Boolean IsChecked { get; set; }
	public Boolean IsOpen { get; set; }
	public Boolean IsNeighbor { get; set; }
	public Boolean IsAttack { get; set; }

	public ePlaceState State { get; set; }

	public Place Top { get; set; }
	public Place Bottom { get; set; }
	public Place Left { get; set; }
	public Place Right { get; set; }

	public Ship CurrentShip { get { return Ship.Current; } }
	public SpriteRenderer SpriteRenderer { get; private set; }

	public void Awake()
	{
		IsOpen = true;
		IsAttack = false;
		IsNeighbor = false;
		IsChecked = false;

		SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

		fsm = new PlaceFSM();

		fsm.AddTransition(ePlaceState.Open, ePlaceState.Open, Open);
		fsm.AddTransition(ePlaceState.Open, ePlaceState.Move, Move);
		fsm.AddTransition(ePlaceState.Open, ePlaceState.Attack, Attack);

		fsm.AddTransition(ePlaceState.Close, ePlaceState.Close, Close);
		fsm.AddTransition(ePlaceState.Close, ePlaceState.Open, Open);

		fsm.AddTransition(ePlaceState.Move, ePlaceState.Move, Move);
		fsm.AddTransition(ePlaceState.Move, ePlaceState.Open, Open);
		fsm.AddTransition(ePlaceState.Move, ePlaceState.Wait, Wait);

		fsm.AddTransition(ePlaceState.Attack, ePlaceState.Attack, Attack);
		fsm.AddTransition(ePlaceState.Attack, ePlaceState.Open, Open);

		fsm.AddTransition(ePlaceState.Wait, ePlaceState.Wait, Wait);
		fsm.AddTransition(ePlaceState.Wait, ePlaceState.Close, Close);
	}

	public void Update()
	{
		var nextPossibleState = GetState();
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
	protected ePlaceState GetState()
	{
		if (IsAttack)
			return ePlaceState.Attack;

		if (IsNeighbor)
			return ePlaceState.Move;

		if (IsOpen)
			return ePlaceState.Open;
		else
			return ePlaceState.Close;
	}

	public void Wait()
	{
		if (SpriteRenderer.sprite != IFactory.PlaceWait)
			SpriteRenderer.sprite = IFactory.PlaceWait;
	}

	public void Move()
	{
		if (SpriteRenderer.sprite != IFactory.PlaceMove)
			SpriteRenderer.sprite = IFactory.PlaceMove;
	}

	public void Attack()
	{
		if (SpriteRenderer.sprite != IFactory.PlaceAttack)
			SpriteRenderer.sprite = IFactory.PlaceAttack;
	}

	public void Open()
	{
		if (SpriteRenderer.sprite != IFactory.PlaceOpen)
			SpriteRenderer.sprite = IFactory.PlaceOpen;
	}

	public void Close()
	{
		if (SpriteRenderer.sprite != IFactory.PlaceClose)
			SpriteRenderer.sprite = IFactory.PlaceClose;
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
