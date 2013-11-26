using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour 
{
	private Vector3 screenPoint;
	private Vector3 offset;
	private Vector3 curScreenPoint;
	private Vector3 curPosition;

	public void OnMouseDown()
	{
		print("OnMouseDown event");
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(
			new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z)
		);

		Screen.showCursor = false;
	}

	public void OnMouseDrag()
	{
		print("OnMouseDrag event");
		curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

		curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
		transform.position = curPosition;
	}

	public void OnMouseUp()
	{
		print("OnMouseUp event");
		Screen.showCursor = true;
	}
}
