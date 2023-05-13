using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    private int healthCount = 3;
    public int maxHealthCount = 3;
    private bool isInvinsible = false;

    public float invinsibleAfterHitTime = 2.0f;
    public bool needInvinsibilityOnHit = false;
    public float lastTimeHit = float.MaxValue;
    void Start()
    {
        healthCount = maxHealthCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInvinsible)
        {
            lastTimeHit += Time.deltaTime;
            if(lastTimeHit > invinsibleAfterHitTime)
            {
                isInvinsible = false;
            }
        }
    }

    

    public void Hit(int hitCount) {

        if (!isInvinsible)
        {
            healthCount -= hitCount;
            healthCount = healthCount > 0 ? healthCount : 0;
            if (needInvinsibilityOnHit)
            {
                lastTimeHit = 0.0f;
                isInvinsible = true;
            }
        }

    }

    public bool IsDead()
    {
        return healthCount == 0;
    }

    public int GetCurrentHealthCount()
    {
        return healthCount;
    }
    public int GetMaxHealthCount()
    {
        return maxHealthCount;
    }

    public bool IsInvincible()
    {
        return isInvinsible;
    }
}
