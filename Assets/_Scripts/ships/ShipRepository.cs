using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ShipRepository : MonoBehaviour
{
    #region Position
    public float start_X;
    public float start_Y;
    public float start_Z;
    #endregion

    #region Prefabs
    public GameObject PF_shipSmall;
    public GameObject PF_shipMiddle;
    public GameObject PF_shipBig;
    #endregion

	private PlayerScript _player;

    public void Start()
    {
		_player = (PlayerScript)gameObject.GetComponent("player");
	    if (_player)
	    {
		    throw new Exception("Player creation error");
	    }
    }

    public GameObject CreateShip(ShipType type, Vector3 position)
    {
	    var prefab = GetPrefabByType(type);

		var result = (GameObject)Instantiate(prefab, position, prefab.transform.rotation);

        return result;
    }

	private GameObject GetPrefabByType(ShipType type)
	{
		GameObject result = null;

		switch (type)
		{
			case ShipType.Small:
				result = PF_shipSmall;
				break;
			case ShipType.Middle:
				result = PF_shipMiddle;
				break;
			case ShipType.Big:
				result = PF_shipBig;
				break;
		}

		return result;
	}
}
