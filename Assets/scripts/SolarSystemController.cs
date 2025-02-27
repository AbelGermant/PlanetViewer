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

    public GameObject sun;
    public GameObject mercury;
    public GameObject venus;
    public GameObject earth;
    public GameObject mars;
    public GameObject jupiter;
    public GameObject saturn;
    public GameObject uranus;
    public GameObject neptune;

    public Dictionary<PlanetData.Planet, GameObject> planetGameObjects = new Dictionary<PlanetData.Planet, GameObject>();

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

        PlanetManager.current.OnTimeChange += UpdatePosition;

        DrawOrbit();
        HideOrbit();

        SetScaleToBig();

    }

    void Update()
    {

    }


    public void UpdatePosition(DateTime t)
    {
        // Loop through all the planets
        foreach (KeyValuePair<PlanetData.Planet, GameObject> planet in planetGameObjects)
        {
            Vector3 position = PlanetData.GetPlanetPosition(planet.Key, t);
            planet.Value.transform.position = position;
        }
    }

    public void SetScaletoRealistic()
    {
        sun.transform.localScale = new Vector3(0.0093f, 0.0093f, 0.0093f);
        mercury.transform.localScale = new Vector3(0.00003f, 0.00003f, 0.00003f);
        venus.transform.localScale = new Vector3(0.00008f, 0.00008f, 0.00008f);
        earth.transform.localScale = new Vector3(0.00008f, 0.00008f, 0.00008f);
        mars.transform.localScale = new Vector3(0.00004f, 0.00004f, 0.00004f);
        jupiter.transform.localScale = new Vector3(0.00093f, 0.00093f, 0.00093f);
        saturn.transform.localScale = new Vector3(0.00077f, 0.00077f, 0.00077f);
        uranus.transform.localScale = new Vector3(0.00033f, 0.00033f, 0.00033f);
        neptune.transform.localScale = new Vector3(0.00032f, 0.00032f, 0.00032f);
    }

    public void SetScaleToBig()
    {
        sun.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        foreach (KeyValuePair<PlanetData.Planet, GameObject> planet in planetGameObjects)
        {
            float scale = planetScale[planet.Key]*0.5f;
            planet.Value.transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    private Dictionary<PlanetData.Planet, float> planetScale = new Dictionary<PlanetData.Planet, float>()
    {
        {PlanetData.Planet.Mercury, 0.1f},
        {PlanetData.Planet.Venus, 0.248f},
        {PlanetData.Planet.Earth, 0.261f},
        {PlanetData.Planet.Mars, 0.1389f},
        {PlanetData.Planet.Jupiter, 2.865f},
        {PlanetData.Planet.Saturn, 2.387f},
        {PlanetData.Planet.Uranus, 1.039f},
        {PlanetData.Planet.Neptune, 1f}
    };

    public void DrawOrbit()
    {
        // Draw the orbit of the planets
        foreach (KeyValuePair<PlanetData.Planet, GameObject> planet in planetGameObjects)
        {
            LineRenderer lineRenderer = planet.Value.GetComponent<LineRenderer>();
            int ndaysInYear = PlanetData.daysInYear[planet.Key];
            lineRenderer.positionCount = 0;

            for (int i = 0; i < ndaysInYear +10; i += (int)(ndaysInYear/100)+1)
            {
                Vector3 position = PlanetData.GetPlanetPosition(planet.Key, new DateTime(2000, 1, 1).AddDays(i));
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(i/((int)(ndaysInYear/100)+1), position);
            }
        }
    }

    public void ShowOrbit()
    {
        foreach (KeyValuePair<PlanetData.Planet, GameObject> planet in planetGameObjects)
        {
            LineRenderer lineRenderer = planet.Value.GetComponent<LineRenderer>();
            lineRenderer.enabled = true;
        }
    }

    public void HideOrbit()
    {
        foreach (KeyValuePair<PlanetData.Planet, GameObject> planet in planetGameObjects)
        {
            LineRenderer lineRenderer = planet.Value.GetComponent<LineRenderer>();
            lineRenderer.enabled = false;
        }
    }
}