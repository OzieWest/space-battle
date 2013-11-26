using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class PlaceController : MonoBehaviour
{
	public float startX;
	public float startY;

	public int gridCount;

	public float placeScale;

	public float offset;

	public GameObject place_prefab;

	private List<GameObject> places;

	public void Start()
	{
		places = new List<GameObject>();

		var startVector = new Vector3(startX, startY);

		for (var i = 0; i < gridCount; i++)
		{
			for (var j = 0; j < gridCount; j++)
			{
				var obj = CreatePlace(startVector);

				startVector.x += placeScale + offset;
			}

			startVector.x = startX;
			startVector.y -= placeScale + offset;
		}
	}

	public void Update()
	{

	}

	private GameObject CreatePlace(Vector3 position)
	{
		var result = Instantiate(place_prefab, position, transform.rotation) as GameObject;
		result.transform.localScale = new Vector3(placeScale, placeScale, 0.1f);

		return result;
	}
}
