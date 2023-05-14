using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    // Start is called before the first frame update

    public float scrollingSpeed = 0.0f;
    Vector2 offset;
    private float scrollValue = 0.0f;
    void Start()
    {
        offset = GetComponent<SpriteRenderer>().material.mainTextureOffset;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            float boosterValue = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<ShootingScript>().GetBoostingLinearlyScaledValue();
            scrollValue = Mathf.Repeat(scrollValue + Time.deltaTime * (scrollingSpeed + boosterValue), 1);
            // to do: scalable scrolling
            Vector2 scrolledOffset = new Vector2(offset.x, scrollValue);
            GetComponent<SpriteRenderer>().material.mainTextureOffset = scrolledOffset;
        }
    }
}
