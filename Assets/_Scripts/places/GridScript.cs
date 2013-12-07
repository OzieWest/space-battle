using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using System.Collections;

public class GridScript : BaseBehaviour<GridScript>
{
	#region Coor
	public float startX;
	public float startY;

	public int gridCount;

	public float placeScale;

	public float offset;
	#endregion

	private List<List<GameObject>> _places;

	public void Start()
	{
		Current = this;

		_places = new List<List<GameObject>>();

		_createGrid();
	}

	public PlaceScript GetRandomLocation()
	{
		Debug.Log("!");

		var firstInt = Random.Range(7, 10);
		var secondInt = Random.Range(0, 10);

		var place = _places[firstInt][secondInt].GetComponent<PlaceScript>();

		if (!place.IsFree)
			place = GetRandomLocation();

		return place;
	}

	private void _createGrid()
	{
		var startVector = new Vector3(startX, startY);

		for (var i = 0; i < gridCount; i++)
		{
			var innerList = new List<GameObject>();

			for (var j = 0; j < gridCount; j++)
			{
				innerList.Add(
					_createPlace(startVector)
				);

				startVector.x += placeScale + offset;
			}

			_places.Add(innerList);

			startVector.x = startX;
			startVector.y -= placeScale + offset;
		}
	}

	private GameObject _createPlace(Vector3 position)
	{
		var result = Inst(PrefabFactory.Current.Place, position, transform.rotation);

		result.transform.localScale = new Vector3(placeScale, placeScale, 0.1f);

		return result;
	}
}
