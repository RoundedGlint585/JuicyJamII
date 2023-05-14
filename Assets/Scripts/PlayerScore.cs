using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    // Start is called before the first frame update

    int score = 0;
    void Start()
    {
        
    }

    public void AddPoints(int points)
    {
        score += points;
    }
    
    public int GetPlayerScore()
    {
        return score;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
