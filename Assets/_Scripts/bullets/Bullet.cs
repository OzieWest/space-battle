using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float speed = 20.0f;
    public float time = 0.0f;

	public void Start () {
	}
	
	public void Update ()
	{
	    Movement();
	    LiveCycle();
	}

    /// <summary>
    /// Движение "снаряда" вперед
    /// </summary>
    public void Movement()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
    }

    /// <summary>
    /// Жизненный цикл "снаряда" после создания
    /// </summary>
    public void LiveCycle()
    {
        time += Time.deltaTime;
        if (time > 2)
        {
            Destroy(gameObject);
        }
    }
}
