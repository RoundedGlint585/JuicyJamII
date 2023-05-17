using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UIElements;


public enum ProjectileType { basic, autoTargeting, exploding};
public class ProjectileBehaviour : MonoBehaviour
{

    public Vector2 direction = new Vector2(0.0f, 1.0f);
    Rigidbody2D rb;
    public string targetTag = "Enemy";
    public float speedMultiplier = 1.0f;
    public int damageValue = 1;
    public GameObject player;
    public ProjectileType type;
    public bool playerSpeedApplied = false;
    private bool directionFixed = false;

    public float autoTargetingForce = 0.2f;

    //explosion projectile behaviour
    public int projectileCountAfterExplosion = 0;
    public float explosionTime = 3.0f;
    public GameObject projectileToSpawn;
    private float lifeTime = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    public Vector2 UpdateDirection()
    {
        if(type == ProjectileType.autoTargeting)
        {
            Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            Vector3 directionCorrection = (playerPosition - transform.position).normalized;
            if (transform.position.y > playerPosition.y && !directionFixed)
            {
                direction = (direction + autoTargetingForce * new Vector2(directionCorrection.x, directionCorrection.y)).normalized;
                return direction;
            }
            else
            {
                directionFixed = true;
            }
        }
        return direction;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDirection();
        rb.velocity = direction * speedMultiplier;
        float angle = -Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        lifeTime += Time.deltaTime;
        if(type == ProjectileType.exploding && lifeTime > explosionTime)
        {
            SpawnProjectilesAround();
            DestroyProjectile();
        }

    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.CompareTag(targetTag))
        {
            collider.transform.gameObject.TryGetComponent<HealthBehaviour>(out HealthBehaviour health);
            collider.transform.gameObject.TryGetComponent<EnemyBehaviour>(out EnemyBehaviour enemy);
            if (health != null)
            {
                health.Hit(damageValue);
                if (health.IsDead())
                {
                    if(targetTag != "Player")
                    {
                        enemy.DestroyEnemy(true);
                    }

                }
            }
            else
            {
                //ds: was basically fix for player with no health component
                enemy.DestroyEnemy();
            }
            DestroyProjectile();
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


    private void SpawnProjectilesAround()
    {
        float step = 360.0f / projectileCountAfterExplosion;
        Quaternion rotStep = Quaternion.Euler(0, 0, step);
        Vector3 updatedDirection = new Vector3(0, 1, 0);
        GameObject gameObj = Instantiate(projectileToSpawn);
        ProjectileBehaviour projectile = gameObj.GetComponent<ProjectileBehaviour>();
        projectile.type = ProjectileType.basic;
        projectile.direction = updatedDirection;
        for (int i = 0; i < projectileCountAfterExplosion-1; i++)
        {
            updatedDirection = rotStep * updatedDirection;
            gameObj = Instantiate(gameObject);
            projectile = gameObj.GetComponent<ProjectileBehaviour>();
            projectile.type = ProjectileType.basic;
            projectile.direction = updatedDirection;
        }
    }
}
