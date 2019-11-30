using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed;
    public float lifeTime;

    public GameObject explosion;

    public GameObject soundObject;

    public int Dmg;

    void Start()
    {
        Invoke("DestoryProjectile", lifeTime);
        Instantiate(soundObject, transform.position, transform.rotation);
    }

    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void DestoryProjectile()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().takeDmg(Dmg);
            DestoryProjectile();
        }

        if(collision.tag == "Boss")
        {
            collision.GetComponent<Boss>().takeDmg(Dmg);
            DestoryProjectile();
        }
    }
}
