using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class ShipRepository : BaseBehaviour<ShipRepository>
{
	private Dictionary<ShipType, Ship> _ships;

	public ShipRepository()
	{
		_ships = new Dictionary<ShipType, Ship>();
	}

	public Ship CreateShip(ShipType type, Vector3 position)
	{
		var prefab = GetPrefabByType(type); // получаем чистый префаб
		var result = Inst(prefab, position, prefab.transform.rotation ); // настраиваем префаб
		var ship = ConfigurateShip(result, type); // настраиваем поведение

		_ships.Add(type, ship);

		return ship;
	}

	public Dictionary<ShipType, Ship> GetAllShips()
	{
		return _ships;
	}

	private Ship ConfigurateShip(GameObject prefab, ShipType type)
	{
		var shipPrefab = prefab.GetComponent<Ship>();
		shipPrefab.Health = GetHealthByType(type);
		shipPrefab.Power = GetPowerByType(type);
		shipPrefab.Action = GetDefaultAction();
		shipPrefab.State = GetDefaultState();
		shipPrefab.Type = type;
		
		return shipPrefab;
	}

	public static GameObject GetPrefabByType(ShipType type)
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
	public static ShipAction GetDefaultAction()
	{
		return ShipAction.Stay;
	}

	//Состояние корабля по умолчанию
	public static ShipState GetDefaultState()
	{
		return ShipState.Alive;
	}

	//Возвращает количество "Здоровья" в зависимости от типа корабля
	public static int GetHealthByType(ShipType type)
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
	public static int GetPowerByType(ShipType type)
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
