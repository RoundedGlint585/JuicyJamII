using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    private int healthCount = 3;
    public int maxHealthCount = 3;
    private bool isInvincible = false;

    public float invincibleAfterHitTime = 2.0f;
    public bool needInvinsibilityOnHit = false;
    public float lastTimeHit = float.MaxValue;
    void Start()
    {
        healthCount = maxHealthCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInvincible)
        {
            lastTimeHit += Time.deltaTime;
            if(lastTimeHit > invincibleAfterHitTime)
            {
                isInvincible = false;
            }
        }
    }

    

    public void Hit(int hitCount) {

        if (!isInvincible)
        {
            healthCount -= hitCount;
            healthCount = healthCount > 0 ? healthCount : 0;
            
            if (needInvinsibilityOnHit)
            {
                GetComponent<Movement>().MoveToTheSpawnPoint(); // should be ok as far as only player invincible
                lastTimeHit = 0.0f;
                isInvincible = true;
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
        return isInvincible;
    }

    public float GetInvincibilityProgress()
    {
        if (isInvincible)
        {
            return Mathf.Clamp01(lastTimeHit / invincibleAfterHitTime);
        }
        return 1.0f; 
    }
}
