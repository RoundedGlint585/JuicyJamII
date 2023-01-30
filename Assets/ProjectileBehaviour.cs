using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{

    public Vector2 direction = new Vector2(0.0f, 1.0f);
    Rigidbody2D rb;
    public string targetTag = "Enemy";
    public float speedMultiplier = 1.0f;
    public int damageValue = 1;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = direction * speedMultiplier;
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.tag == targetTag)
        {
            HealthBehaviour health;
            collider.transform.gameObject.TryGetComponent<HealthBehaviour>(out health);
            if (health != null)
            {
                health.Hit(damageValue);
                if (health.IsDead())
                {
                    Destroy(collider.gameObject);
                }
            }
            else
            {
                Destroy(collider.gameObject);
            }
            Destroy(gameObject);
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
