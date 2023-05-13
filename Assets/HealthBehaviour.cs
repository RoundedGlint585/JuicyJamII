using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    private int healthCount = 3;
    public int maxHealthCount = 3;
    void Start()
    {
        healthCount = maxHealthCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void Hit(int hitCount) {
        healthCount -= hitCount;
        healthCount = healthCount > 0 ? healthCount : 0;
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
}
