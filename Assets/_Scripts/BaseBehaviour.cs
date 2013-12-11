using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseBehaviour<U> : MonoBehaviour where U: class 
{
	public static U Current { get; set; }

	public PrefabFactory PFactory { get { return PrefabFactory.Current; } }
	public ImageFactory IFactory { get { return ImageFactory.Current; } }
	public Player Player { get { return Player.Current; } }

	public GameObject Inst(UnityEngine.Object prefab, Vector3 position, Quaternion rotation)
	{
		return (GameObject)Instantiate(prefab, position, rotation);
	}

	public U Inst<U>(UnityEngine.Object prefab, Vector3 position, Quaternion rotation) where U : Component
	{
		var newObj = (GameObject)Instantiate(prefab, position, rotation);
		return newObj.GetComponent<U>();
	}

	public void SetColor(Color color)
	{
		gameObject.renderer.material.color = color;
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

	public Quaternion Rotation
	{
		get
		{
			return gameObject.transform.rotation;
		}
		set
		{
			gameObject.transform.rotation = value;
		}
	}

	public Boolean IsShipSelect()
	{
		return Ship.Current != null;
	}
}
