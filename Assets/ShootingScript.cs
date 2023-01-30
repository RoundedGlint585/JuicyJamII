using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public float shootingCooldown = 0.2f;

    private float lastTimeShooted;

    public GameObject projectileObject;
    public AnimationCurve speedCurve;
    float speedModifier = 1.0f;

    public float speedBoosterMaxTime = 10.0f;
    public float speedBoosterTimer = 0.0f;

    public float speedBoosterCooldownStartDelay = 1.0f; // delay before boosting lowering after mouse button unclicked
    public float speedBoosterCooldownDelayTimer = 0.0f;



    // Start is called before the first frame update
    void Start()
    {
        lastTimeShooted = shootingCooldown + 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        lastTimeShooted += Time.deltaTime; 
        if (Input.GetMouseButton(0))
        {
            speedBoosterCooldownDelayTimer = 0.0f;
            speedBoosterTimer += Time.deltaTime;
            if (lastTimeShooted > shootingCooldown)
            {
                lastTimeShooted = 0.0f;
                CreateProjectiles();
            }
        } else {
            speedBoosterCooldownDelayTimer += Time.deltaTime;
            if (speedBoosterCooldownDelayTimer > speedBoosterCooldownStartDelay)
            {
                speedBoosterTimer -= Time.deltaTime;
                speedBoosterTimer = Mathf.Max(speedBoosterTimer, 0.0f);

            }
        }

    }

    void CreateProjectiles()
    {
        Vector3 position = transform.GetChild(0).position;
        GameObject gameObj = Instantiate(projectileObject);
        gameObj.transform.position = position;
        gameObj.GetComponent<ProjectileBehaviour>().direction = new Vector2(0.0f, 1.0f);
        gameObj.GetComponent<ProjectileBehaviour>().targetTag = "Enemy";
        gameObj.GetComponent<ProjectileBehaviour>().speedMultiplier = 1 + GetBoostingValue();
    }


    // value on animation curve
    public float GetBoostingValue()
    {
        float speedMultiplier = speedCurve.Evaluate(GetBoostingLinearlyScaledValue());
        return speedMultiplier;
    }
    //value scaled from 0 to 1
    public float GetBoostingLinearlyScaledValue()
    {
        float speedBoosterTimerScaled = Mathf.Clamp(speedBoosterTimer, 0, speedBoosterMaxTime);
        speedBoosterTimerScaled /= speedBoosterMaxTime;
        return speedBoosterTimerScaled;
    }
}
