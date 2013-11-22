using UnityEngine;
using System.Collections;

public class PlayerSelect : MonoBehaviour {

    public void OnMouseDown()
    {
        renderer.material.color = Color.red;
    }

    public void OnMouseUp()
    {
        renderer.material.color = Color.white;
    }

    public void Update()
    {
        if (Camera.current != null)
        {
            print("!");
        }
        else
        {
            print("?");
        }

        if (Input.GetMouseButtonDown(0))
        {
            

            var hitInfo = new RaycastHit();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo) && hitInfo.transform.tag == "Construction")
            {
                print("It's working");
            }
        }
    }
}
