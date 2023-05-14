using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;

    public float speed = 1.0f;

    public float offset = 0.01f;

    private Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private float tilt = 0f;
    public float tiltSpeed = 5;
    public float tiltReturnSpeed = 1;
    void UpdateTiltAnimation(float horizontalInput)
    {
        tilt = Mathf.MoveTowards(tilt, 0, Time.deltaTime * tiltReturnSpeed);
        tilt += horizontalInput * Time.deltaTime * tiltSpeed;
        tilt = Mathf.Clamp(tilt, -1, 1);

        animator.SetFloat("Horizontal", tilt);
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 position = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().WorldToViewportPoint(transform.position);
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        UpdateTiltAnimation(horizontal);

        if((position.x < 0.0f + offset && horizontal < 0.0f) || (position.x > 1.0f - offset && horizontal > 0.0f))
        {
            
            rb.constraints = rb.constraints | RigidbodyConstraints2D.FreezePositionX;
        }
        else
        {
            if(!rb.constraints.HasFlag(RigidbodyConstraints2D.FreezePositionY))
            {
                rb.constraints = RigidbodyConstraints2D.None;
            }
            else
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            }  
        }
        if ((position.y <= 0.0f + offset && vertical < 0.0f) || (position.y >= 1.0f - offset && vertical > 0.0f))
        {

            rb.constraints = rb.constraints | RigidbodyConstraints2D.FreezePositionY;
        }
        else
        {
            if (!rb.constraints.HasFlag(RigidbodyConstraints2D.FreezePositionX))
            {
                rb.constraints = RigidbodyConstraints2D.None;
            }
            else
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            }

        }
        Vector3 direction = new Vector3(horizontal, vertical, 0);
        rb.velocity = new Vector2(horizontal * speed, vertical * speed);
    }

    public void MoveToTheSpawnPoint()
    {
        GetComponent<Rigidbody2D>().position = new Vector2(0.0f, 0.0f);
    }
}
