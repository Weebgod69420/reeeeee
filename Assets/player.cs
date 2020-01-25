using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public Rigidbody2D thisRB;
    [SerializeField]
    float speed;
    [SerializeField]
    LayerMask whatIsGround;
    bool grounded;
    int jumps = 2;

    void Start()
    {
        
    }

    void Update()
    {
        if(grounded && thisRB.velocity.y < 1)
        {
            float horizontal = Input.GetAxis("Horizontal");
            thisRB.velocity = new Vector2(horizontal * speed, thisRB.velocity.y);
        }
        grounded = false;
        if (Physics2D.OverlapCircle(transform.position - new Vector3(0, 0.35f, 0), 0.2f, whatIsGround) != null)
        {
            jumps = 2;
            grounded = true;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && jumps > 0)
        {
            Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector3 pos = transform.position;
            thisRB.velocity = (new Vector2(pos.x,pos.y) - mousePosition).normalized * 10;
            jumps--;
        }
    }
}
