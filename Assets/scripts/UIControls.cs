using System;
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
        PlanetManager.current.SetDeltaDays(-1);
        BPause.gameObject.SetActive(true);
        BPlay.gameObject.SetActive(false);
    }

    public void Forward()
    {
        PlanetManager.current.SetDeltaDays(4);
        BPause.gameObject.SetActive(true);
        BPlay.gameObject.SetActive(false);
    }
}