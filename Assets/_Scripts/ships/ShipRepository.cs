using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ShipRepository : BaseBehaviour<ShipRepository>
{
    #region Position
    public float start_X;
    public float start_Y;
    public float start_Z;
    #endregion

	public List<GameObject> Ships;
    
	public void Start()
    {
	    Current = this;

		Ships = new List<GameObject>();
    }

    public GameObject CreateShip(ShipType type, Vector3 position)
    {
	    var prefab = GetPrefabByType(type);

		var result = (GameObject)Instantiate(prefab, position, prefab.transform.rotation);

		Ships.Add(result);

        return result;
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
}
