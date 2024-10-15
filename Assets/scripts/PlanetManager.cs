using System;
using UnityEngine;
public class PlanetManager : MonoBehaviour
{
    public static PlanetManager current;

    public event Action<DateTime> OnTimeChange;

    [SerializeField]
    private UDateTime date;




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
        Date = Date.dateTime.AddDays(1);
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
}