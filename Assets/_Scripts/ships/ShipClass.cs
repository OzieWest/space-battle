using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum ShipType
{
	Unknown = 0,
	Small,
	Middle,
	Big
}

public enum ShipAction
{
	Unknown = 0,
	Move,
	Stay,
	Fire
}

public enum ShipState
{
	Unknown = 0,
	Alive,
	Wounded,
	Dead
}

public class ShipClass
{
	public int Id { get; set; }
	
	public ShipType Type { get; set; }
	public ShipAction Action { get; set; }
	public ShipState State { get; set; }

	public int Health { get; set; }
	public int Power { get; set; }

	public ShipClass()
	{
		Id = 0;
		
		Type = ShipType.Unknown;
		Action = ShipAction.Unknown;
		State = ShipState.Unknown;
		
		Power = 0;
		Health = 0;
	}

	public Boolean IsAlive()
	{
		return Health > 0;
	}
}
