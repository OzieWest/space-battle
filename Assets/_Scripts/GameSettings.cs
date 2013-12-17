using System;
using UnityEngine;
using System.Collections;

public class GameSettings : BaseBehaviour<GameSettings>
{
	public GameSettings() { Current = this; }

	public Boolean IsDebug = true;

	public static void Log(String message)
	{
		if (Current.IsDebug)
		{
			Debug.Log(message);
		}
	}
}
