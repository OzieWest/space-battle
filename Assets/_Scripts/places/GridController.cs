using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class GridController : BaseBehaviour<GridController>
{
	#region Coor
	private int gridX = 10;
	private int gridY = 8;

	public float placeScale = 1;
	public float offset = 1;
	#endregion

	public Sprite IconDefault;
	public Sprite IconMove;
	public Sprite IconAttack;

	private List<List<Place>> _places;

	public Ship CurrentShip { get { return Ship.Current; } }

	public void Start()
	{
		Current = this;

		_places = new List<List<Place>>();

		_createGrid();

		_configuratePlace();
	}

	public void Update()
	{
		if (IsShipSelect())
		{
			var places = CurrentShip.Place.GetNeighbors();
			foreach (var place in places)
			{
				place.SetSprite(IconMove, Color.blue);
			}

			foreach (var place in this)
			{
				if (place.GetSprite() != IconMove && place.GetSprite() != IconAttack && place.IsFree)
				{
					place.SetSprite(IconAttack, Color.red);
				}
			}
		}
		else
		{
			ResetSprites();
		}
	}

	public void ResetSprites()
	{
		foreach (var place in this)
		{
			if (place.GetSprite() != IconDefault)
			{
				place.SetSprite(IconDefault, Color.white);
			}
		}
	}

	private void _configuratePlace()
	{
		for (var i = 0; i < _places.Count; i++)
		{
			for (var j = 0; j < _places[i].Count; j++)
			{
				var place = _places[i][j];

				if (i != 0)
				{
					place.Left = _places[i - 1][j];
				}

				if (i < _places.Count - 1)
				{
					place.Right = _places[i + 1][j];
				}

				if (j != 0)
				{
					place.Top = _places[i][j - 1];
				}

				if (j < _places[i].Count - 1)
				{
					place.Bottom = _places[i][j + 1];
				}
			}
		}
	}

	public IEnumerator<Place> GetEnumerator()
	{
		return _places.SelectMany(placeList => placeList).GetEnumerator();
	}

	public Place GetRandomLocation()
	{
		var firstInt = Random.Range(7, 10);
		var secondInt = Random.Range(0, 8);

		var place = _places[firstInt][secondInt];

		if (!place.IsFree)
			place = GetRandomLocation();

		return place;
	}

	private void _createGrid()
	{
		var startVector = Camera.main.ScreenToWorldPoint(new Vector3(50, Screen.height - 50, 20));
		var startX = startVector.x;

		for (var i = 0; i < gridX; i++)
		{
			var innerList = new List<Place>();

			for (var j = 0; j < gridY; j++)
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

	public Boolean IsShipSelect()
	{
		return CurrentShip != null;
	}

	private Place _createPlace(Vector3 position)
	{
		var result = Inst(PrefabFactory.Current.Place, position, transform.rotation);

		var place = result.GetComponent<Place>();
		place.SetSprite(IconDefault, Color.white);

		return place;
	}
}
