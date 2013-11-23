using UnityEngine;
using System.Collections;

public class Place : MonoBehaviour 
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ship")
        {
            
        }
    }
}
