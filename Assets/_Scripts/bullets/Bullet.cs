using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;

public class Bullet : BaseBehaviour<Bullet>
{
	private float _moveSpeed = 2f;
	private float time = 0.0f;
	public Vector3 EndPosition { get; set; }

	public void Start()
	{
		
	}

	public void Update ()
	{
	    Movement();
	    //LiveCycle();
	}

    public void Movement()
    {
		if (EndPosition != Vector3.zero)
		{
			Position = Vector3.Slerp(Position, EndPosition, Time.deltaTime * _moveSpeed);

			if (Position == EndPosition)
			{
				EndPosition = Vector3.zero;
				Destroy(gameObject);
			}
		}
    }

    public void LiveCycle()
    {
        time += Time.deltaTime;
        if (time > 2)
        {
            Destroy(gameObject);
        }
    }
}
