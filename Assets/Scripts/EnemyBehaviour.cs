using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public float lifeTimeLimit = 10.0f;
    public Vector3 positionToMove = new Vector3(0, 0.8f, 2);
    public float speed = 1.0f;
    float lifeTime = 0.0f;
    bool isAtPlace = false;
    public int pointsForKill = 10;
    public float shootingCooldownTime = 1.0f;


    public ProjectileType projectileType = ProjectileType.basic;

    public int projectileCount = 1;

    public Vector3 basicProjectileDirection = new Vector3 { x = 0, y = -1, z = 0 };

    // angle spread for new projectiles
    public float minAngle = 0.0f;
    public float maxAngle = 0.0f;

    //explosive projectile settings
    public int projectileCountInCaseOfExplosionProjectile = 3;
    public float explosiveProjectileLifeTime = 3.0f;

    public GameObject projectileObject;

    private PlayerScore playerScore;


    private float shootingTimer = 0.0f;
    void Start()
    {
        shootingTimer = shootingCooldownTime;

        playerScore = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<PlayerScore>();
    }

    public void DestroyEnemy(bool needAddPoints = false)
    {
        if (needAddPoints)
        {
            playerScore.AddPoints(pointsForKill);
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        shootingTimer += Time.deltaTime;

        if (lifeTime > lifeTimeLimit)
        {
            // explode
            // (ds): do we need to add score for this one?
            DestroyEnemy();
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
                SpawnProjectiles();
                shootingTimer = 0.0f;
            }
        }

    }


    GameObject CreateProjectile()
    {
        float boostingValue = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<ShootingScript>().GetBoostingValue();
        Vector3 position = transform.GetChild(0).position;
        GameObject gameObj = Instantiate(projectileObject);
        gameObj.transform.position = position;
        ProjectileBehaviour projectile = gameObj.GetComponent<ProjectileBehaviour>();
        projectile.direction = basicProjectileDirection;
        projectile.speedMultiplier = 1 + boostingValue;
        projectile.targetTag = "Player";
        projectile.type = projectileType;
        projectile.projectileCountAfterExplosion = projectileCountInCaseOfExplosionProjectile;
        projectile.explosionTime = explosiveProjectileLifeTime;
        return gameObj;
    }
    
    void SpawnProjectilePair(float step, int index)
    {
        GameObject gameObj = CreateProjectile();
        ProjectileBehaviour projectile = gameObj.GetComponent<ProjectileBehaviour>();
        Quaternion rot = Quaternion.Euler(0, 0, step * (index + 1));
        projectile.direction = rot * basicProjectileDirection;

        gameObj = CreateProjectile();
        projectile = gameObj.GetComponent<ProjectileBehaviour>();
        rot = Quaternion.Euler(0, 0, -step * (index + 1));
        projectile.direction = rot * basicProjectileDirection;
    }
    void SpawnProjectiles()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            float angle = Random.Range(minAngle, maxAngle);
            float step = 2 * angle / projectileCount;
            if (projectileCount % 2 == 1)
            {
                CreateProjectile(); // straight to basic direction
            }
            for (int i = 0; i < projectileCount / 2; i++)
            {
                SpawnProjectilePair(step, i);
            }
            

        }
    }
}
