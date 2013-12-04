using UnityEngine;
using System.Collections;

public class PrefabFactory : BaseBehaviour<PrefabFactory>
{
	public GameObject Bullet;
	
	public GameObject SmallShip;
	public GameObject MiddleShip;
	public GameObject BigShip;
	
	public GameObject Place;

	public GameObject SelectionRing;
	public GameObject IconMove;
	public GameObject IconAttack;

	public void Start()
	{
		Current = this;
	}
}
