using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public float cooldown = 0.2f;

    private float lastTimeShooted;

    public GameObject projectileObject;

    float speedModifier = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        lastTimeShooted = cooldown + 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        lastTimeShooted += Time.deltaTime; 
        if (Input.GetMouseButton(0) && lastTimeShooted > cooldown )
        {
            lastTimeShooted = 0.0f;
            CreateProjectiles();
        }

    }

    void CreateProjectiles()
    {
        Vector3 position = transform.GetChild(0).position;
        GameObject gameObj = Instantiate(projectileObject);
        gameObj.transform.position = position;
        gameObj.GetComponent<ProjectileBehaviour>().direction = new Vector2(0.0f, 1.0f);
        gameObj.GetComponent<ProjectileBehaviour>().targetTag = "Enemy";
    }
}
