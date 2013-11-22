using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public float rotateSpeed = 5;

    public void Update()
    {
        float a = 0;
        if (Input.GetMouseButton(0))
            a -= rotateSpeed;
        else if (Input.GetMouseButton(1))
            a += rotateSpeed;

        //transform.RotateAround(Vector3.zero, Vector3.up, a * Time.deltaTime);
    }
}
