using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public int health = 100;
    public float speed = 12.0f;
    public float padding = 1f;
    public float shootDelay = 0.2f;
    public float laserOffset = 0.5f;
    public Sprite customAmmoSprite;
    public Sprite noMovement;
    public Sprite rightMovement;
    public Sprite leftMovement;
    public GameObject ammo;
    public GameObject deathObject;

    private float xmin;
    private float xmax;

	void Start () {
        LoadSprite(this.noMovement);

        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xmin = leftMost.x + padding;
        xmax = rightMost.x - padding;
    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKey(KeyCode.LeftArrow))
        {
            LoadSprite(this.leftMovement);
            // Verbose way of writing the code below
            // transform.position += new Vector3((-speed * Time.deltaTime), 0, 0);

            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            LoadSprite(this.rightMovement);
            // Verbose way
            // transform.position += new Vector3((speed * Time.deltaTime), 0, 0);

            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else
        {
            LoadSprite(this.noMovement);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Shoot", 0.00001f, shootDelay);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Shoot");
        }

        // Restricts the player to the game space
        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidingObject = collision.gameObject;
        Laser laser = collidingObject.GetComponent<Laser>();
        if (laser)
        {
            int damage = laser.Hit(this.gameObject);
            health -= damage;
            if (health <= 0)
            {
                Die();
            }
        }
    }

    void LoadSprite (Sprite newSprite)
    {
        Sprite currentSprite = this.GetComponent<SpriteRenderer>().sprite;
        if (currentSprite != newSprite)
        {
            this.GetComponent<SpriteRenderer>().sprite = newSprite;
        }
    }

    void Shoot ()
    {
        Laser laser = ammo.GetComponent<Laser>();
        laser.firedBy = this.gameObject;

        if(customAmmoSprite)
        {
            laser.sprite = customAmmoSprite;
        }
        Vector3 laserOrigin = transform.position;
        laserOrigin.y = laserOrigin.y + laserOffset;
        Instantiate(laser, laserOrigin, Quaternion.identity);
    }

    void Die()
    {
        Destroy(this.gameObject);
        Instantiate(deathObject, transform.position, Quaternion.identity);
    }

}
