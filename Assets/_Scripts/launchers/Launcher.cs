using UnityEngine;
using System.Collections;

public class Launcher : MonoBehaviour
{
	public GameObject bulletPrefab;

	public void Start()
	{
	
	}
	
	public void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Instantiate(bulletPrefab, transform.position, transform.rotation);
		}
	}
}
