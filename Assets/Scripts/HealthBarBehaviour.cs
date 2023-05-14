using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehaviour : MonoBehaviour
{
    private GameObject player;


    public GameObject healthPointObject;

    public int offset = 25;
    public GameObject[] healthPoints;
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        int maxHealthPointsCount = player.GetComponent<HealthBehaviour>().GetMaxHealthCount();
        healthPoints = new GameObject[maxHealthPointsCount];
        for (int i = 0; i < maxHealthPointsCount; i++)
        {
            healthPoints[i] = Instantiate(healthPointObject, transform.parent);
            Vector3 position = transform.position;
            healthPoints[i].transform.SetParent(transform);
            position.x += (healthPointObject.GetComponent<Image>().sprite.texture.width + offset) * i;
            healthPoints[i].transform.position = position;
            healthPoints[i].transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        int maxHealthPointsCount = player.GetComponent<HealthBehaviour>().GetMaxHealthCount();
        int currentLifePoints = player.GetComponent<HealthBehaviour>().GetCurrentHealthCount();
        if (currentLifePoints < 0)
        {
            return;
        }
        for (int i = 0; i < currentLifePoints; i++)
        {
            healthPoints[i].GetComponent<HealthPointBehaviour>().EnableLife();
        }
        for (int i = currentLifePoints; i < maxHealthPointsCount; i++)
        {
            healthPoints[i].GetComponent<HealthPointBehaviour>().DisableLife();
        }
    }
}