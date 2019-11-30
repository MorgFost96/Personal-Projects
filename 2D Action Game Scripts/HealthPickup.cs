using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour {

    Player playerScript;
    public int healAmt;
    public GameObject effect;

    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Instantiate(effect, transform.position, Quaternion.identity);

            playerScript.Heal(healAmt);
            Destroy(gameObject);
        }
    }



}
