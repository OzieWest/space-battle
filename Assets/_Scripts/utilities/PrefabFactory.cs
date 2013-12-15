using UnityEngine;
using System.Collections;

public class PrefabFactory : BaseBehaviour<PrefabFactory>
{
	public GameObject Bullet;
	
	public GameObject SmallShip;
	public GameObject MiddleShip;
	public GameObject BigShip;
	
	public GameObject Place;

	public GameObject enemySmallShip;
	public GameObject enemyMiddleShip;
	public GameObject enemyBigShip;

	public void Start()
	{
		Current = this;
	}
}
