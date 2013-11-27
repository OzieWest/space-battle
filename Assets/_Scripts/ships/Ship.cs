using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour 
{
	private Vector3 screenPoint;
	private Vector3 offset;
	private Vector3 curScreenPoint;
	private Vector3 curPosition;

	public Vector3 prevPosition;
	public Vector3 newPosition;

	public void OnMouseDown()
	{
		prevPosition = transform.position;

		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(
			new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z)
		);
	}

	public void OnMouseDrag()
	{
		curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

		curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		transform.position = curPosition;

		Screen.showCursor = false;
	}

	public void OnMouseUp()
	{
		if (newPosition != Vector3.zero)
		{
			var position = newPosition;
			position.y += .5f;
			position.z += -.2f;

			transform.position = position;
		}
		else
		{
			transform.position = prevPosition;
		}

		Screen.showCursor = true;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Place")
		{
			newPosition = other.gameObject.transform.position;
		}
		else
		{
			newPosition = Vector3.zero;
		}
	}
}
