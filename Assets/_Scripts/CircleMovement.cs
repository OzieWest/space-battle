﻿using UnityEngine;
using System.Collections;

public class CircleMovement : MonoBehaviour
{
    public float rotateAroundSpeed;
    public float rotateInnerSpeed;

    public void Update () {
        transform.RotateAround(Vector3.zero, Vector3.up, rotateAroundSpeed * Time.deltaTime);

        transform.Rotate(
            Vector3.up,
            rotateInnerSpeed
        );
    }
}
