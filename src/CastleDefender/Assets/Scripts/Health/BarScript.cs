using System;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{

    [SerializeField]
    private bool lerpColors;
    [SerializeField]
    private Image content;
    [SerializeField]
    private Text valueText;
    [SerializeField]
    private float lerpSpeed;

    private float fillAmount;

    [SerializeField]
    private Color fullColor;

    [SerializeField]
    private Color lowColor;

    public float MaxValue { get; set; }


    public float Value
    {
        set
        {
            fillAmount = Map(value, 0, MaxValue, 0, 1);
        }
    }

    void Start()
    {
        if (lerpColors) 
        {
            content.color = fullColor;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        
        HandleBar();

    }

    /// <summary>
    /// Updates the bar
    /// </summary>
    private void HandleBar()
    {
        if (fillAmount != content.fillAmount) 
        {
            //Lerps the fill amount so that we get a smooth movement
            content.fillAmount = Mathf.Lerp(content.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);

            if (lerpColors) 
            {   
               
                content.color = Color.Lerp(lowColor, fullColor, fillAmount);
            }
           
        }
    }
    public void ResetHealth()
    {
        content.fillAmount = 1;
        Value = MaxValue;
    }

    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
