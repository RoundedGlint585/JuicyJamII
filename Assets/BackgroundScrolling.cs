using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    // Start is called before the first frame update

    public float scrollingSpeed = 0.5f;
    Vector2 offset;
    public AnimationCurve scrollingCurve;
    void Start()
    {
        offset = GetComponent<SpriteRenderer>().material.mainTextureOffset;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            float boosterMaxTime = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<ShootingScript>().speedBoosterMaxTime;
            float boosterTimer = Mathf.Max(GameObject.FindGameObjectWithTag("Player").transform.GetComponent<ShootingScript>().speedBoosterTimer, 0);
            boosterTimer = Mathf.Min(boosterMaxTime, boosterTimer);

            float boosterCoefficient = boosterTimer / boosterMaxTime;

            float y = Mathf.Repeat(Time.time * (scrollingSpeed), 1);
            // to do: scalable scrolling
            Vector2 scrolledOffset = new Vector2(offset.x, y);
            GetComponent<SpriteRenderer>().material.mainTextureOffset = scrolledOffset;
        }
    }
}
