using UnityEngine;
using System.Collections;

public class SelectionRingScript : MonoBehaviour {

	public void Update () 
	{
		transform.Rotate(
			Vector3.forward,
			0.5f
		);
	}
}
