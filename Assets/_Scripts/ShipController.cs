using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour
{
    #region Position
    public float start_X;
    public float start_Y;
    public float start_Z;
    #endregion

    #region Prefabs
    public GameObject p_ship1;
    public GameObject p_ship2;
    public GameObject p_ship3;
    public GameObject p_ship4;
    #endregion

    public void Start()
    {
        var ship = CreateShip(p_ship1, new Vector3(start_X, start_Y, start_Z));
    }

    private GameObject CreateShip(GameObject ship, Vector3 position)
    {
        var result = Instantiate(ship, position, Quaternion.identity) as GameObject;
    
        return result;
    }
}
