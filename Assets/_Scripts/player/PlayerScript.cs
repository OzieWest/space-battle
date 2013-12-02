using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
	public String Name;
	public int Score;
	public List<ShipClass> Ships;
	public List<GameObject> ShipsGO;

	public ShipClassController ShipClassController { get; set; }
	public ShipRepository ShipRepository { get; set; }

	public void OnGUI()
	{
		GUI.Label(new Rect(5, 5, 150, 20), "Name: " + Name);
		GUI.Label(new Rect(5, 25, 150, 20), "Score: " + Score.ToString());
		GUI.Label(new Rect(5, 45, 150, 20), "Ships: " + Ships.Count.ToString());
	}

	public void Start()
	{
		//TODO create injector
		ShipClassController = new ShipClassController();
		Ships = ShipClassController.GetAllShip();

		ShipRepository = (ShipRepository)gameObject.GetComponent("ShipRepository");
		if (ShipRepository == null)
		{
			throw new Exception("Player creation error");
		}

		CreateFleat();
	}

	public void CreateFleat()
	{
		var vector = Vector3.zero;

		foreach (var ship in Ships)
		{
			ShipsGO.Add(
				ShipRepository.CreateShip(ship.Type, vector)
			);
		}
	}
}
