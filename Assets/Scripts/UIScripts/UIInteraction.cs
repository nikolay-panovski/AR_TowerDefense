using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInteraction : MonoBehaviour
{
    //activate button
    public GameObject activate;
    //sell+ability button
    public GameObject sell;
    //active tower image
    public GameObject imageActive;
    //inactive tower image
    public GameObject imageInactive;

    public void Start ()
    {
        activate.SetActive(false);
        sell.SetActive(false);
        imageActive.SetActive(false);
        imageInactive.SetActive(true);
    }

    public void towerSelect()
    {
        activate.SetActive(true);
    }

    public void towerActive()
    {
        activate.SetActive(false);
        sell.SetActive(true);
        imageActive.SetActive(true);
        imageInactive.SetActive(false);
    }
}
