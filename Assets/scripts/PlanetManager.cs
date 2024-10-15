using System;
using UnityEngine;
public class PlanetManager : MonoBehaviour
{
    public static PlanetManager current;

    public event Action<DateTime> OnTimeChange;

    [SerializeField]
    private UDateTime date;

    private int deltaDays = 1;




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

    void Start()
    {
        Date = DateTime.Now;
    }


    void Update()
    {
        Date = Date.dateTime.AddDays(deltaDays);
    }



    public void TimeChanged(DateTime newTime)
    {
        OnTimeChange?.Invoke(newTime);
    }


    public UDateTime Date
    {
        get => date;
        set
        {
            date = value;
            TimeChanged(value.dateTime); //Fire the event
        }
    }

    public void SetDate(DateTime newDate)
    {
        Date = newDate;
    }

    public void SetDeltaDays(int days)
    {
        deltaDays = days;
    }
}