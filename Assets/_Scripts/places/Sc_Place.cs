using UnityEngine;
using System.Collections;

public class Sc_Place : MonoBehaviour 
{
	public void OnMouseOver()
	{
		gameObject.renderer.material.color = Color.black;
	}

	public void OnMouseExit()
	{
		gameObject.renderer.material.color = new Color(102f, 195f, 255f);
	}
}
