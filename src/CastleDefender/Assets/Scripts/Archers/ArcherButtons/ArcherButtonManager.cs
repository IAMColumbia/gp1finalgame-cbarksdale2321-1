using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArcherButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject archerPrefab;

    [SerializeField] private int price;

    [SerializeField] private Text priceText;

    public GameObject ArcherPrefab
       {
        get => archerPrefab;
       }
    public int Price
    {
        get
        {
            return price;
        }

        }

    private void Start()
    {
        priceText.text = Price + " Respect";
    }
}
