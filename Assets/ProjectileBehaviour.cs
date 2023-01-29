using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{

    public Vector2 direction = new Vector2(0.0f, 1.0f);
    Rigidbody2D rb;
    public string targetTag = "Enemy";
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = direction;
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.tag == targetTag)
        {
            Destroy(collider.gameObject);
            Destroy(gameObject);
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
