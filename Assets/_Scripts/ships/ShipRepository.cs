using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class ShipRepository
{
	private List<GameObject> _ships;
	private PlayerScript _playerScript;

	public ShipRepository(PlayerScript playerScript)
    {
		_ships = new List<GameObject>();
		_playerScript = playerScript;
    }

    public GameObject CreateShip(ShipType type, Vector3 position)
    {
	    var prefab = GetPrefabByType(type);

		var result = _playerScript.Inst(
			prefab, 
			position, 
			prefab.transform.rotation
		);

		_ships.Add(
			_configurateShip(result, type)
		);

        return result;
    }

	public List<GameObject> GetAllShips()
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
		};

		var shipPrefab = prefab.GetComponent<ShipScript>();

		shipPrefab.Struct = shipStruct;

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
			case ShipType.Middle:
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
			case ShipType.Middle:
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
			case ShipType.Middle:
				result = 2;
				break;
			case ShipType.Big:
				result = 3;
				break;
		}

		return result;
	}
}
