﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class ShipRepository
{
	private Dictionary<ShipType, GameObject> _ships;
	private PlayerScript _player;

	public ShipRepository(PlayerScript playerScript)
	{
		_ships = new Dictionary<ShipType, GameObject>();
		_player = playerScript;
    }

    public GameObject CreateShip(ShipType type, Vector3 position)
    {
	    var prefab = GetPrefabByType(type);

		var result = _player.Inst(
			prefab, 
			position, 
			prefab.transform.rotation
		);

		_ships.Add(
			type,
			_configurateShip(result, type)
		);

        return result;
    }

	public Dictionary<ShipType, GameObject> GetAllShips()
	{
		return _ships;
	}

	private GameObject _configurateShip(GameObject prefab, ShipType type)
	{
		var shipStruct = new ShipStruct()
		{
			Health = GetHealthByType(type),
			Power = GetPowerByType(type),
			Action = DefaultAction(),
			State = DefaultState(),
			Type = type
		};

		var shipPrefab = prefab.GetComponent<ShipScript>();

		shipPrefab.Struct = shipStruct;

		prefab.active = false;

		return prefab;
	}

	private GameObject GetPrefabByType(ShipType type)
	{
		GameObject result = null;

		switch (type)
		{
			case ShipType.Small:
				result = PrefabFactory.Current.SmallShip;
				break;
			case ShipType.Medium:
				result = PrefabFactory.Current.MiddleShip;
				break;
			case ShipType.Big:
				result = PrefabFactory.Current.BigShip;
				break;
		}

		return result;
	}

	//Действие корабля по умолчанию
	private ShipAction DefaultAction()
	{
		return ShipAction.Stay;
	}

	//Состояние корабля по умолчанию
	private ShipState DefaultState()
	{
		return ShipState.Alive;
	}

	//Возвращает количество "Здоровья" в зависимости от типа корабля
	private int GetHealthByType(ShipType type)
	{
		var result = 0;

		switch (type)
		{
			case ShipType.Small:
				result = 1;
				break;
			case ShipType.Medium:
				result = 2;
				break;
			case ShipType.Big:
				result = 3;
				break;
		}

		return result;
	}

	//Возвращает количество "Силы" в зависимости от типа корабля
	private int GetPowerByType(ShipType type)
	{
		var result = 0;

		switch (type)
		{
			case ShipType.Small:
				result = 1;
				break;
			case ShipType.Medium:
				result = 2;
				break;
			case ShipType.Big:
				result = 3;
				break;
		}

		return result;
	}
}
