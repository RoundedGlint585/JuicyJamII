using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyWaveBehaviour : MonoBehaviour
{

    public GameObject prevWave = null;
    private EnemyWaveBehaviour prevEnemyWaveBehaviour = null;
    public int enemiesLeftBeforeSpawnDelayStart = 0;
    public float spawnDelay = 2.0f;
    private float time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (prevWave != null)
        {
            prevEnemyWaveBehaviour = prevWave.GetComponent<EnemyWaveBehaviour>();
        }
        DisableEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        if(prevWave == null || prevEnemyWaveBehaviour.GetEnemiesCountLeft() <= enemiesLeftBeforeSpawnDelayStart)
        {
            time += Time.deltaTime;
            if(time > spawnDelay)
            {
                ActivateEnemies();
            }
        }
    }
    public void ActivateEnemies()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.gameObject.SetActive(true);
        }
    }

    public void DisableEnemies()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.gameObject.SetActive(false);
        }
    }
    public int GetEnemiesCountLeft()
    {
        return transform.childCount;
    }
}
