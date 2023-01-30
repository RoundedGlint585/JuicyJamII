using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public float lifeTimeLimit = 10.0f;
    public Vector3 positionToMove = new Vector3(0, 0.8f, 2);
    public float speed = 1.0f;
    float lifeTime = 0.0f;
    bool isAtPlace = false;

    public float shootingCooldownTime = 1.0f;

    public float shootingTimer = 0.0f;

    public GameObject projectileObject;
    void Start()
    {
        shootingTimer = shootingCooldownTime;
    }

    // Update is called once per frame
    void Update()
    {
        shootingTimer += Time.deltaTime;

        if (lifeTime > lifeTimeLimit)
        {
            // explode
            Destroy(gameObject);
        }

        if (!isAtPlace)
        {
            Vector3 directionVector = positionToMove - transform.position;
            if (directionVector.magnitude < 0.001f)
            {
                isAtPlace = true;
                transform.position = positionToMove;
            }
            directionVector.Normalize();
            transform.position = transform.position + directionVector * speed * Time.deltaTime;
        }

        if (isAtPlace)
        {
            lifeTime += Time.deltaTime;
            if (shootingTimer > shootingCooldownTime)
            {
                SpawnProjectile();
                shootingTimer = 0.0f;
            }
        }

    }

    void SpawnProjectile()
    {
        float boostingValue = GameObject.FindGameObjectsWithTag("Player")[0].transform.GetComponent<ShootingScript>().GetBoostingValue();
        Vector3 position = transform.GetChild(0).position;
        GameObject gameObj = Instantiate(projectileObject);
        gameObj.transform.position = position;
        gameObj.GetComponent<ProjectileBehaviour>().direction = new Vector2(0.0f, -1.0f);
        gameObj.GetComponent<ProjectileBehaviour>().speedMultiplier = 1 + boostingValue;
        gameObj.GetComponent<ProjectileBehaviour>().targetTag = "Player";
    }
}
