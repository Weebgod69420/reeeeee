using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    Rigidbody2D thisRB;
    [SerializeField]
    float speed,recoil;
    [SerializeField]
    LayerMask whatIsGround;
    bool grounded;
    int jumps = 2;
    Transform gun,barrel;
    [SerializeField]
    GameObject bullet, victoryText;
    void Start()
    {
        thisRB = GetComponent<Rigidbody2D>();
        gun = transform.GetChild(0);
        barrel = gun.GetChild(0);
    }

    void Update()
    {
        grounded = false;
        if (Physics2D.OverlapCircle(transform.position - new Vector3(0, 0.35f, 0), 0.2f, whatIsGround) != null && thisRB.velocity.y <= 0)
        {
            jumps = 2;
            grounded = true;
        }
        if (grounded && thisRB.velocity.y < 1)
        {
            float horizontal = Input.GetAxis("Horizontal");
            if (grounded)
            {
                thisRB.velocity = new Vector2(horizontal * speed, thisRB.velocity.y);
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && jumps > 0)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 pos = transform.position;
            thisRB.velocity = (new Vector2(pos.x, pos.y) - mousePosition).normalized * recoil;
            if (grounded)
            {
                thisRB.velocity = new Vector2(thisRB.velocity.x, Mathf.Clamp(thisRB.velocity.y, 3, Mathf.Infinity));
            }   
            shoot();
            jumps--;
        }
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        gun.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            die();
        }
    }
    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
    void shoot()
    {
        GameObject newBullet = Instantiate(bullet, barrel.position, gun.rotation);
        Rigidbody2D bulletRB = newBullet.GetComponent<Rigidbody2D>();
        bulletRB.velocity = newBullet.transform.right * -10;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            die();
        }
    }
    void die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "goal")
        {
            victoryText.SetActive(true);
        }
    }
}