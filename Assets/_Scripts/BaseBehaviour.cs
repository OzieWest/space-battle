using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
		{
			obj.active = true;
		}
	}

	public void TurnOff(String key)
	{
		var obj = Get(key);

		if (obj != null)
		{
			obj.active = false;
		}
	}

	public void SetColor(Color color)
	{
		gameObject.renderer.material.color = color;
	}
}
