using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;

public class BulletScript : BaseBehaviour<BulletScript>
{
    public float speed = 20.0f;
    public float time = 0.0f;

	public void Update ()
	{
	    Movement();
	    LiveCycle();
	}

    public void Movement()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
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
