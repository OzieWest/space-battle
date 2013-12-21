using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class PlaceController : BaseBehaviour<PlaceController>
{
	#region Coor
	protected const int Columns = 6;
	protected const int Rows = 10;
	protected const float PlaceScale = 1;
	protected const float Offset = 1;
	#endregion

	protected List<List<Place>> Places;

	public Ship CurrentShip { get { return Ship.Current; } }

	public void Awake()
	{
		Current = this; 
		Places = new List<List<Place>>();
	}

	public void Start()
	{
		_сreateGrid();
		_сonfiguratePlace();
	}

	public void OnGUI()
	{
		var count = 0;
		foreach (var place in this)
		{
			if (!place.IsOpen)
				count++;
		}

		GUI.Label(new Rect(0, 0, 200, 50), count.ToString());
	}

	public void Update()
	{
		if (IsShipSelect())
		{
			//attack range
			foreach (var place in this)
			{
				if (place.IsOpen)
				{
					place.IsAttack = true;
					place.IsNeighbor = false;
				}
			}

			//move range
			var movementPlaces = CurrentShip.CurrentLocation.GetNeighbors().Where(x => x.IsOpen);
			foreach (var movementPlace in movementPlaces)
			{
				movementPlace.IsNeighbor = true;
				movementPlace.IsAttack = false;
			}
		}
		else
		{
			SetDefaultSprites();
		}
	}

	public void SetDefaultSprites()
	{
		foreach (var place in this)
		{
			place.IsNeighbor = false;
			place.IsAttack = false;
		}
	}

	public Place GetRandomPlace()
	{
		var firstInt = Random.Range(7, 10);
		var secondInt = Random.Range(0, 6);

		var place = Places[firstInt][secondInt];

		if (!place.IsOpen)
			place = GetRandomPlace();

		return place;
	}

	#region Support Method
	public IEnumerator<Place> GetEnumerator()
	{
		return Places.SelectMany(placeList => placeList).GetEnumerator();
	}

	public Vector3 GetCoordinateLastRow()
	{
		var place = Places[Rows - 1][0];
		return place.Position;
	}
	#endregion

	#region Init
	protected void _сreateGrid()
	{
		var startVector = Camera.main.ScreenToWorldPoint(new Vector3(50, Screen.height - 50, 20));
		var startX = startVector.x;

		for (var i = 0; i < Rows; i++)
		{
			var innerList = new List<Place>();

			for (var j = 0; j < Columns; j++)
			{
				var newPlace = _сreatePlace(startVector);
				innerList.Add(newPlace);

				startVector.x += PlaceScale + Offset;
			}

			Places.Add(innerList);

			startVector.x = startX;
			startVector.y -= PlaceScale + Offset;
		}
	}

	protected Place _сreatePlace(Vector3 position)
	{
		var result = Inst(PrefabFactory.Current.Place, position, transform.rotation);

		var place = result.GetComponent<Place>();
		place.SetSprite(IFactory.PlaceOpen);

		return place;
	}

	protected void _сonfiguratePlace()
	{
		for (var i = 0; i < Places.Count; i++)
		{
			for (var j = 0; j < Places[i].Count; j++)
			{
				var place = Places[i][j];

				if (i != 0)
				{
					place.Left = Places[i - 1][j];
				}

				if (i < Places.Count - 1)
				{
					place.Right = Places[i + 1][j];
				}

				if (j != 0)
				{
					place.Top = Places[i][j - 1];
				}

				if (j < Places[i].Count - 1)
				{
					place.Bottom = Places[i][j + 1];
				}
			}
		}
	}
	#endregion
}
