using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public enum PlanetPosition
{
    Near = 0,
    Middle = 1,
    Far = 2
}

public class PlanetController : MonoBehaviour
{
    public float Distance = 0;
    public float Angle = 360;

    public float globalSpeed = 100;
    public List<Material> materials;

    public GameObject planet_prefab;

    public void Start()
    {
        Angle = Angle * Mathf.Deg2Rad;
        float position = 0;
        float planetSize = 0;

        for (int i = 0; i < materials.Count; i++)
        {
            var component = (CircleMovement)planet_prefab.GetComponent(typeof(CircleMovement));
            if (component != null)
            {
                if (i == 0) //PlanetPosition.Near
                {
                    SetPrefab(component, 2.0f, 2.5f);
                    planetSize = Random.Range(1f, 1.5f);
                }
                else if ((materials.Count - 1) == i) // PlanetPosition.Far
                {
                    SetPrefab(component, 0.1f, 0.5f);
                    planetSize = Random.Range(2.2f, 2.6f);
                }
                else // PlanetPosition.Middle
                {
                    SetPrefab(component, 0.5f, 1.1f);
                    planetSize = Random.Range(1.6f, 1.9f);
                }
            }

            planet_prefab.renderer.material = materials[i];
            planet_prefab.transform.localScale = new Vector3(planetSize, planetSize, planetSize);

            SetPlanetsPosition(planet_prefab, materials.Count, i);
        }
    }

    void SetPrefab(CircleMovement component, float x, float y)
    {
        component.rotateAroundSpeed = Random.Range(x * globalSpeed * Time.deltaTime, y * globalSpeed * Time.deltaTime);
        component.rotateInnerSpeed = Random.Range(x * globalSpeed * Time.deltaTime, y * globalSpeed * Time.deltaTime);
    }

    public void SetPlanetsPosition(GameObject planet, int count, int index)
    {
        float _x = transform.position.x + Mathf.Sin(Angle / count * index) * Distance;
        float _y = transform.position.y + Mathf.Cos(Angle / count * index) * Distance;

        Vector3 point = transform.position;
        point.x = _x;
        point.y = _y;

        Instantiate(planet_prefab, point, Quaternion.identity);

        Distance += 5;
    }
}
