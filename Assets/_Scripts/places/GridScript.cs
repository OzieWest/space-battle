using System.Collections.Generic;
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

	private List<GameObject> _places;

	public void Start()
	{
		Current = this;

		_places = new List<GameObject>();

		_createGrid();
	}

	private void _createGrid()
	{
		var startVector = new Vector3(startX, startY);

		for (var i = 0; i < gridCount; i++)
		{
			for (var j = 0; j < gridCount; j++)
			{
				_createPlace(startVector);

				startVector.x += placeScale + offset;
			}

			startVector.x = startX;
			startVector.y -= placeScale + offset;
		}
	}

	private void _createPlace(Vector3 position)
	{
		var result = Inst(PrefabFactory.Current.Place, position, transform.rotation);

		result.transform.localScale = new Vector3(placeScale, placeScale, 0.1f);

		_places.Add(result);
	}
}
