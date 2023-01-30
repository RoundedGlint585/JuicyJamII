using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        float boostingValue = GameObject.FindGameObjectsWithTag("Player")[0].transform.GetComponent<ShootingScript>().GetBoostingLinearlyScaledValue();
        slider.value = boostingValue;
    }
}
