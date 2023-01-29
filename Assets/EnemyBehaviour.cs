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
    Rigidbody2D rb;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isAtPlace)
        {
            lifeTime += Time.deltaTime;
        }
        if(lifeTime > lifeTimeLimit)
        {
            // explode
            Destroy(gameObject);
        }
        rb = GetComponent<Rigidbody2D>();
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

    }
}
