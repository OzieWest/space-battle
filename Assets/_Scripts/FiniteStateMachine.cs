﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void Callback();

public class StateTransition<S> : System.IEquatable<StateTransition<S>>
{
	protected S mInitState;
	protected S mEndState;

	public StateTransition() { }
	public StateTransition(S init, S end) { mInitState = init; mEndState = end; }

	public bool Equals(StateTransition<S> other)
	{
		if (ReferenceEquals(null, other))
			return false;
		if (ReferenceEquals(this, other))
			return true;

		return mInitState.Equals(other.GetInitState()) && mEndState.Equals(other.GetEndState());
	}

	public override int GetHashCode()
	{
		if ((mInitState == null || mEndState == null))
			return 0;

		unchecked
		{
			int hash = 17;
			hash = hash * 23 + mInitState.GetHashCode();
			hash = hash * 23 + mEndState.GetHashCode();
			return hash;
		}
	}

	public S GetInitState() { return mInitState; }
	public S GetEndState() { return mEndState; }
}

public class FiniteStateMachine<S>
{
	protected S mState;
	protected S mPrevState;
	protected bool mbLocked = false;

	protected Dictionary<StateTransition<S>, System.Delegate> mTransitions;

	public FiniteStateMachine()
	{
		mTransitions = new Dictionary<StateTransition<S>, System.Delegate>();
	}

	public void Initialise(S state) { mState = state; }

	public S Advance(S nextState)
	{
		if (mbLocked) return mState;

		var transition = new StateTransition<S>(mState, nextState);

		System.Delegate d;
		if (mTransitions.TryGetValue(transition, out d)) // new StateTransition(mState, nextState)
		{
			if (d != null)
			{
				Callback c = d as Callback;
				c();
			}

			mPrevState = mState;
			mState = nextState;
		}

		return mState;
	}

	public void AddTransition(S init, S end, Callback c)
	{
		var tr = new StateTransition<S>(init, end);

		if (mTransitions.ContainsKey(tr)) return;

		mTransitions.Add(tr, c);
	}

	public void Lock() { mbLocked = true; }

	public void Unlock()
	{
		mbLocked = false;
		Advance(mPrevState);
	}

	public S GetState() { return mState; }
	public S GetPrevState() { return mPrevState; }
}