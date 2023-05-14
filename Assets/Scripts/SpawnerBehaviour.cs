using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject enemyToSpawn;
    Vector3 positionToMove = new Vector3(0.0f, 0.8f, 2.0f);
    float spawnCooldown = 6.0f;
    float spawnTimer = 0.0f;
    void Start()
    {
        positionToMove.x = transform.position.x;
        spawnTimer = spawnCooldown + 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer > spawnCooldown)
        {
            spawnTimer = 0.0f;
            Instantiate(enemyToSpawn);
            enemyToSpawn.GetComponent<EnemyBehaviour>().positionToMove = positionToMove;
        }

    }
}
