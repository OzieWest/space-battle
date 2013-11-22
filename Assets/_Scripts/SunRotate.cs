using UnityEngine;
using System.Collections;

public class SunRotate : MonoBehaviour
{
    public float speed;

    public void Update()
    {
        transform.Rotate(
            Vector3.up,
            speed
        );
    }
}
