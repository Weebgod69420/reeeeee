using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public int health;
    int direction;
    Rigidbody2D thisRB;
    [SerializeField]
    LayerMask whatIsGround;
    [SerializeField]
    float speed;
    void Start()
    {
        direction = (Random.Range(0, 2) * 2)- 1;
        thisRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        thisRB.velocity = new Vector2(direction * speed, thisRB.velocity.y);
        if (Physics2D.OverlapCircle(transform.position + new Vector3(direction * 0.6f,-0.6f,0),0.2f,whatIsGround) == null)
        {
            flip();
        }
        if (Physics2D.OverlapCircle(transform.position + new Vector3(direction * 1f, 0, 0), 0.2f, whatIsGround) != null)
        {
            flip();
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    void flip()
    {
        direction *= -1;
    }
}
