using System;
using System.Collections.Generic;
using TMPro;
using UI.Dates;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIControls : MonoBehaviour
{

    public static UIControls current;

    public DatePicker datePicker;

    public Button BPause;
    public Button BPlay;
    public Button BBackward;
    public Button BForward;

    public TextMeshProUGUI InfoText;

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

    // Start is called before the first frame update
    void Start()
    {
        // Initialization code here
        //PlanetManager.current.OnTimeChange += SetSelectedDate;
    }

    // Update is called once per frame
    void Update()
    {
        // Frame update code here
    }


    public void ToggleRealistScale(Boolean isRealistic)
    {
        if (isRealistic)
        {
            // Realistic scale
            SolarSystemController.current.SetScaletoRealistic();
        }
        else {
            // Non-realistic scale
            SolarSystemController.current.SetScaleToBig();
        }
    }

    public void ToggleOrbit(Boolean isOrbit)
    {
        if (isOrbit)
        {
            // Show orbits
            SolarSystemController.current.ShowOrbit();
        }
        else{
            // Hide orbits
            SolarSystemController.current.HideOrbit();
        }
    }

    public void SetSelectedDate(DateTime date)
    {
        datePicker.SelectedDate = date;
    }

    public void SetDate()
    {
        try {
            DateTime selectedDate = datePicker.SelectedDate.Date;
            PlanetManager.current.SetDate(selectedDate);
        } catch (Exception e) {
        }
    }

    public void Play()
    {
        PlanetManager.current.SetDeltaDays(1);
        BPause.gameObject.SetActive(true);
        BPlay.gameObject.SetActive(false);
    }

    public void Pause()
    {
        PlanetManager.current.SetDeltaDays(0);
        BPause.gameObject.SetActive(false);
        BPlay.gameObject.SetActive(true);
    }

    public void Backward()
    {
        PlanetManager.current.SetDeltaDays(-4);
        BPause.gameObject.SetActive(true);
        BPlay.gameObject.SetActive(false);
    }

    public void Forward()
    {
        PlanetManager.current.SetDeltaDays(4);
        BPause.gameObject.SetActive(true);
        BPlay.gameObject.SetActive(false);
    }


    public void CenterCamera()
    {
        CameraControl.current.CenterCamera();
    }

    public void ShowInfo(PlanetData.Planet planet)
    {
        InfoText.gameObject.SetActive(true);
        InfoText.text = planetInfo[planet];
    }

    public void HideInfo()
    {
        InfoText.gameObject.SetActive(false);
        InfoText.text = "";
    }

    private Dictionary<PlanetData.Planet, String> planetInfo = new Dictionary<PlanetData.Planet, String>()
    {
        {PlanetData.Planet.Mercury, "Mercury is the smallest planet in the Solar System and the closest to the Sun."},
        {PlanetData.Planet.Venus, "Venus is the second planet from the Sun and the hottest planet in the Solar System."},
        {PlanetData.Planet.Earth, "Earth is the third planet from the Sun and the only planet known to have life."},
        {PlanetData.Planet.Mars, "Mars is the fourth planet from the Sun and the second smallest planet in the Solar System."},
        {PlanetData.Planet.Jupiter, "Jupiter is the fifth planet from the Sun and the largest planet in the Solar System."},
        {PlanetData.Planet.Saturn, "Saturn is the sixth planet from the Sun and the second largest planet in the Solar System."},
        {PlanetData.Planet.Uranus, "Uranus is the seventh planet from the Sun and the third largest planet in the Solar System."},
        {PlanetData.Planet.Neptune, "Neptune is the eighth planet from the Sun and the fourth largest planet in the Solar System."}
    };
}