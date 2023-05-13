using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreRender : MonoBehaviour
{
    TMP_Text text;
    PlayerScore playerScore;
    // Start is called before the first frame update
    void Start()
    {
       text = gameObject.GetComponent<TMP_Text>();
       playerScore = GameObject.FindGameObjectsWithTag("Player")[0].transform.GetComponent<PlayerScore>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = playerScore.GetPlayerScore().ToString();
    }
}
