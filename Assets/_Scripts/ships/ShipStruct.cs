using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ShipStruct : BaseBehaviour<ShipStruct>
{
	public int Id { get; set; }
	
	public ShipType Type { get; set; }
	public ShipAction Action { get; set; }
	public ShipState State { get; set; }

	public int Health;
	public int Power { get; set; }

	public void Start()
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

	public void SetHealth(int a)
	{
		Health = a;
		print("Health method: " + Health);
	}

	public void GetHealth()
	{
		print("Health: " + Health);
	}
}
