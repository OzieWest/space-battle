using System;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;

public enum eBulletState
{
	Move,
	Explode
}

public class BulletFSM : FiniteStateMachine<eBulletState> { };

public class Bullet : BaseBehaviour<Bullet>
{
	protected BulletFSM fsm;

	public Vector3 EndPosition { get; set; }

	protected void Start()
	{
		fsm = new BulletFSM();

		fsm.AddTransition(eBulletState.Move, eBulletState.Move, Move);
		fsm.AddTransition(eBulletState.Move, eBulletState.Explode, Explode);
	}

	protected void Update()
	{
		if (EndPosition == Vector3.zero)
			fsm.Advance(eBulletState.Explode);
		else
			fsm.Advance(eBulletState.Move);
	}

	public void SendTo(Vector3 position)
	{
		EndPosition = position;
	}

	protected void Move()
	{
		float _moveSpeed = 2f;
		Position = Vector3.Slerp(Position, EndPosition, Time.deltaTime * _moveSpeed);
	}

	protected void Explode()
	{
		Destroy(gameObject);
	}
}
