using System;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystemController : MonoBehaviour
{

    public static SolarSystemController current;

    private void Awake()
    {
        if (current == null)
        {
            current = this;
        }
        else
        {
            Destroy(obj: this);
        }
    }


    public GameObject mercury;
    public GameObject venus;
    public GameObject earth;
    public GameObject mars;
    public GameObject jupiter;
    public GameObject saturn;
    public GameObject uranus;
    public GameObject neptune;

    public Dictionary<PlanetData.Planet, GameObject> planetGameObjects = new Dictionary<PlanetData.Planet, GameObject>();

    private DateTime currentTime;

    void Start()
    {
        // Add all the planets to the dictionary
        planetGameObjects.Add(PlanetData.Planet.Mercury, mercury);
        planetGameObjects.Add(PlanetData.Planet.Venus, venus);
        planetGameObjects.Add(PlanetData.Planet.Earth, earth);
        planetGameObjects.Add(PlanetData.Planet.Mars, mars);
        planetGameObjects.Add(PlanetData.Planet.Jupiter, jupiter);
        planetGameObjects.Add(PlanetData.Planet.Saturn, saturn);
        planetGameObjects.Add(PlanetData.Planet.Uranus, uranus);
        planetGameObjects.Add(PlanetData.Planet.Neptune, neptune);

        // Set the current time to the current time
        currentTime = DateTime.Now;
    }

    void Update()
    {
        UpdatePlanetPositions();
    }

    public void UpdatePlanetPositions()
    {
        // Loop through all the planets
        foreach (KeyValuePair<PlanetData.Planet, GameObject> planet in planetGameObjects)
        {
            Vector3 position = PlanetData.GetPlanetPosition(planet.Key, currentTime);
            planet.Value.transform.position = position;
        }

        // Increment the time
        currentTime = currentTime.AddDays(1);
    }
}