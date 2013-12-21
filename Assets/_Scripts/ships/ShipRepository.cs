using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class ShipRepository : BaseBehaviour<ShipRepository>
{
	private Dictionary<eShipType, Ship> _ships;

	public ShipRepository()
	{
		_ships = new Dictionary<eShipType, Ship>();
	}

	public Ship CreateShip(eShipType type, Vector3 position)
	{
		var prefab = GetPrefabByType(type); // получаем чистый префаб
		var result = Inst(prefab, position, prefab.transform.rotation ); // настраиваем префаб
		var ship = ConfigurateShip(result, type); // настраиваем поведение

		_ships.Add(type, ship);

		return ship;
	}

	public Dictionary<eShipType, Ship> GetAllShips()
	{
		return _ships;
	}

	private Ship ConfigurateShip(GameObject prefab, eShipType type)
	{
		var shipPrefab = prefab.GetComponent<Ship>();
		shipPrefab.Health = GetHealthByType(type);
		shipPrefab.Power = GetPowerByType(type);
		shipPrefab.State = GetDefaultAction();
		shipPrefab.Type = type;
		
		return shipPrefab;
	}

	public static GameObject GetPrefabByType(eShipType type)
	{
		GameObject result = null;

		switch (type)
		{
			case eShipType.Small:
				result = PrefabFactory.Current.SmallShip;
				break;
			case eShipType.Medium:
				result = PrefabFactory.Current.MiddleShip;
				break;
			case eShipType.Big:
				result = PrefabFactory.Current.BigShip;
				break;
		}

		return result;
	}

	//Действие корабля по умолчанию
	public static eShipState GetDefaultAction()
	{
		return eShipState.Wait;
	}

	//Возвращает количество "Здоровья" в зависимости от типа корабля
	public static int GetHealthByType(eShipType type)
	{
		var result = 0;

		switch (type)
		{
			case eShipType.Small:
				result = 1;
				break;
			case eShipType.Medium:
				result = 2;
				break;
			case eShipType.Big:
				result = 3;
				break;
		}

		return result;
	}

	//Возвращает количество "Силы" в зависимости от типа корабля
	public static int GetPowerByType(eShipType type)
	{
		var result = 0;

		switch (type)
		{
			case eShipType.Small:
				result = 1;
				break;
			case eShipType.Medium:
				result = 2;
				break;
			case eShipType.Big:
				result = 3;
				break;
		}

		return result;
	}
}
