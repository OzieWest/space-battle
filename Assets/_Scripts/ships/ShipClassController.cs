using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

public class ShipClassController
{
	public List<ShipClass> _ships;

	private int _startId;

	public ShipClassController()
	{
		_ships = new List<ShipClass>();

		_startId = 1;

		_ships.Add(CreateShip(ShipType.Small));
		_ships.Add(CreateShip(ShipType.Middle));
		_ships.Add(CreateShip(ShipType.Big));
	}

	public List<ShipClass> GetAllShip()
	{
		return _ships;
	}

	public List<ShipClass> GetAliveShips()
	{
		return _ships.Where(x => x.IsAlive()).ToList();
	}

	public ShipClass CreateShip(ShipType type)
	{
		var id = _startId++;

		var ship = new ShipClass()
		{
			Id = id,
			Type = type,
			Action = DefaultAction(),
			State = DefaultState(),
			Health = GetHealthByType(type),
			Power = GetPowerByType(type)
		};

		return ship;
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
