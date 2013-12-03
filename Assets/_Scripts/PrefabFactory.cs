using UnityEngine;
using System.Collections;

public class PrefabFactory : MonoBehaviour
{
	public GameObject _bullet;
	public static GameObject Bullet;

	public GameObject _smallShip;
	public static GameObject SmallShip;

	public GameObject _middleShip;
	public static GameObject MiddleShip;

	public GameObject _bigShip;
	public static GameObject BigShip;
	
	public GameObject _selectionRing;
	public static GameObject SelectionRing;

	public GameObject _place;
	public static GameObject Place;

	public void Start()
	{
		Bullet = _bigShip;

		SmallShip = _smallShip;
		MiddleShip = _middleShip;
		BigShip = _bigShip;

		SelectionRing = _selectionRing;

		Place = _place;
	}
}
