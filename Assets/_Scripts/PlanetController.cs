using System;
using System.Collections.Generic;
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
    public float globalSpeed;
    public List<Material> materials;
    public List<GameObject> planets;

    public GameObject planet_prefab;

    public void Start()
    {
        planets = new List<GameObject>();

        float position = 0;
        float planetSize = 0;

        for (int i = 0; i < materials.Count; i++)
        {
            var component = (CircleMovement)planet_prefab.GetComponent(typeof(CircleMovement));
            if (component != null)
            {
                if (i == 0) //PlanetPosition.Near
                {
                    SetPrefab(component, 2f, 5f);
                    position += Random.Range(6f, 8.5f);
                    planetSize = Random.Range(1f, 1.5f);
                }
                else if ((materials.Count - 1) == i) // PlanetPosition.Far
                {
                    SetPrefab(component, 0.5f, 0.8f);
                    position += Random.Range(13.0f, 15.5f);
                    planetSize = Random.Range(2.9f, 3.3f);
                }
                else // PlanetPosition.Middle
                {
                    SetPrefab(component, 0.3f, 2.0f);
                    position += Random.Range(9.9f, 11.6f);
                    planetSize = Random.Range(1.9f, 2.5f);
                }
            }

            planet_prefab.renderer.material = materials[i];
            planet_prefab.transform.localScale = new Vector3(planetSize, planetSize, planetSize);

            var planet = Instantiate(planet_prefab, new Vector3(position, 0, 0), Quaternion.identity) as GameObject;
            planet.name = i.ToString();

            planets.Add(
                planet
            );
        }
    }

    public void OnGUI()
    {
        var startPosition = 10;

        foreach (var planet in planets)
        {
            var position = planet.transform.position;

            GUI.Box(new Rect(10, startPosition, 300, 20),
                String.Format("Planet: {0} -> X: {1} | Z: {2}", planet.name, position.x, position.z)
            );

            startPosition += 40;
        }
    }

    void SetPrefab(CircleMovement component, float x, float y)
    {
        component.rotateAroundSpeed = Random.Range(x * globalSpeed, y * globalSpeed);
        component.rotateInnerSpeed = Random.Range(x * globalSpeed, y * globalSpeed);
    }
}
