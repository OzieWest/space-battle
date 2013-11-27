using UnityEngine;
using System.Collections;

public class Place : MonoBehaviour 
{
    public void OnTriggerEnter(Collider other)
    {
		print("OnTriggerEnter");
        if (other.tag == "Ship")
        {
            
        }
    }
}
