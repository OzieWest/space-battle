using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : BaseBehaviour<PlayerScript>
{
	public String Name;
	public int Score;
	public ShipClassController ShipClassController { get; set; }

	public void Start()
	{
		Current = this;

		ShipClassController = new ShipClassController();

		CreateFleat();
	}

	public void OnGUI()
	{
		GUI.Label(new Rect(5, 5, 150, 20), "Name: " + Name);
		GUI.Label(new Rect(5, 25, 150, 20), "Score: " + Score.ToString());
	}

	public void CreateFleat()
	{
		var ships = ShipClassController.GetAllShip();

		foreach (var ship in ships)
			ShipRepository.Current.CreateShip(ship.Type, Vector3.zero);
	}
}
