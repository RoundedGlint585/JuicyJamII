using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    public int healthCount = 3;
    void Start()
    {
        
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
}
