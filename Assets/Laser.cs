using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
    public Sprite sprite;
    public GameObject firedBy;
    public float speed = 15f;
    public int direction = 1;
    public int damage = 50;

	// Use this for initialization
	void Start () {
        this.GetComponent<SpriteRenderer>().sprite = sprite;
	}
	
	// Update is called once per frame
	void Update () {
        float velocity = speed * direction;
        transform.position += new Vector3(0, (velocity * Time.deltaTime), 0);
	}

    public int Hit (GameObject objectHit)
    {
        if (objectHit != firedBy)
        {
            Destroy(this.gameObject);
            return damage;
        }
        else
        {
            return 0;
        }
    }
}
