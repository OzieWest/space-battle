using System;
using UnityEngine;
using System.Collections;

public class ConfigLevel1 : MonoBehaviour {

	public static Boolean GameIsStart { get; set; }
	private static String startBtnMessage = "Game paused";

	void Start () {
	
	}

	public void OnGUI()
	{
		GameIsStart = GUI.Toggle(new Rect(600, 10, 150, 20), GameIsStart, startBtnMessage);
		
		if(GameIsStart)
		{
			startBtnMessage = "Game started";
		}
		else 
		{
			startBtnMessage = "Game paused";
		}
	}

}
