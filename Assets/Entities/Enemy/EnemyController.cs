using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public static int enemiesRemaining = 0;

    public int health = 100;
    public int damage = 100;
    public float shotsPerSecond = 0.5f;
    public Sprite customAmmoSprite;
    public Sprite enemy;
    public GameObject ammo;
    public GameObject deathObject;

    private float laserOffset = 0.5f;

	void Start () {
        enemiesRemaining += 1;
        this.GetComponent<SpriteRenderer>().sprite = enemy;
    }

    private void Update()
    {
        Shoot(Time.deltaTime);
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

    void Shoot(float deltaTime)
    {
        bool shouldFire = deltaTime * shotsPerSecond > Random.value;
        if (shouldFire)
        {
            Laser laser = ammo.GetComponent<Laser>();
            laser.firedBy = this.gameObject;
            laser.direction = -1;
            laser.sprite = customAmmoSprite;

            Vector3 laserOrigin = transform.position;
            laserOrigin.y = laserOrigin.y - laserOffset;
            Instantiate(laser, laserOrigin, Quaternion.identity);
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
        Instantiate(deathObject, transform.position, Quaternion.identity);
        enemiesRemaining -= 1;
    }
}
