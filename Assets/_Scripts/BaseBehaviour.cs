using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

public class BaseBehaviour<U> : MonoBehaviour where U: class 
{
	public static U Current { get; set; }
	public Dictionary<String, object> Objects { get; set; }

	public BaseBehaviour()
	{
		Objects = new Dictionary<string, object>();
	}
	
	public void Put(String key, object value)
	{
		Objects.Add(key, value);
	}

	public GameObject Get(String key)
	{
		if (Objects.ContainsKey(key))
		{
			return (GameObject)Objects[key];
		}

		return null;
	}

	public T Get<T>(String key) where T : class 
	{
		if (Objects.ContainsKey(key))
		{
			return (T)Objects[key];
		}

		return null;
	}

	public GameObject Inst(UnityEngine.Object prefab, Vector3 position, Quaternion rotation)
	{
		return (GameObject)Instantiate(prefab, position, rotation);
	}

	public void TurnOn(String key)
	{
		var obj = Get(key);

		if (obj != null)
			obj.SetActive(true);
	}

	public void TurnOff(String key)
	{
		var obj = Get(key);

		if (obj != null)
			obj.SetActive(false);
	}

	public void SetColor(Color color)
	{
		gameObject.renderer.material.color = color;
	}

	public void SetPosition(GameObject gameObject, Vector3 position)
	{
		gameObject.transform.position = position;
	}

	public Vector3 Position
	{
		get
		{
			return gameObject.transform.position;
		}
		set
		{
			gameObject.transform.position = value;
		}
	}
}
