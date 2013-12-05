using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class ShipRepository : BaseBehaviour<ShipRepository>
{
	private List<GameObject> _ships;
    
	public void Start()
    {
	    Current = this;

		_ships = new List<GameObject>();
    }

    public GameObject CreateShip(ShipType type, Vector3 position)
    {
	    var prefab = GetPrefabByType(type);

		SetUpStruct(ref prefab, type);

		var result = (GameObject)Instantiate(prefab, position, prefab.transform.rotation);

		_ships.Add(result);

        return result;
    }

	public List<GameObject> GetAllShips()
	{
		return _ships;
	}

	public void SetUpStruct(ref GameObject prefab, ShipType type)
	{
		var shipStr = prefab.GetComponent<ShipStruct>();
		shipStr.Health = GetHealthByType(type);
		shipStr.Power = GetPowerByType(type);
		shipStr.Action = DefaultAction();
		shipStr.State = DefaultState();
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
