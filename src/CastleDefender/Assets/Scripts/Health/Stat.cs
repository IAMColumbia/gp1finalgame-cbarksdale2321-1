using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
class Stat
{

    [SerializeField]
    private BarScript bar;

    public BarScript Bar
    {
        get
        {
            return bar;
        }
    }


    [SerializeField]
    private float maxVal;


    [SerializeField]
    private float currentVal;


    public float CurrentValue
    {
        get
        {
            return currentVal;
        }
        set
        {
            //Clamps the current value between 0 and max
            this.currentVal = Mathf.Clamp(value, 0, MaxVal);

            //Updates the bar
            bar.Value = currentVal;
        }
    }


    public float MaxVal
    {
        get
        {
            return maxVal;
        }
        set
        {
            //Updates the bar's max value
            bar.MaxValue = value;

            //Sets the max value
            this.maxVal = value;
        }
    }


    public void Initialize()
    {
        //Updates the bar
        this.MaxVal = maxVal;
        this.CurrentValue = currentVal;
    }
}

