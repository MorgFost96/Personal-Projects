using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour {

    public int Health;
    public int damage;
    public Enemy[] enemies;
    public float spawnOffset;  
    private int halfHealth;
    private Animator anim;
    public GameObject blood;
    public GameObject effect;
    private Slider healthBar;

    private SceneTransition Win;

    private void Start()
    {
        halfHealth = Health / 2;
        anim = GetComponent<Animator>();
        healthBar = FindObjectOfType<Slider>();
        healthBar.maxValue = Health;
        healthBar.value = Health;
        Win = FindObjectOfType<SceneTransition>();
    }

    public void takeDmg(int dmgAmt)
    {
        Health -= dmgAmt;
        healthBar.value = Health;
        if (Health <= 0)
        {
            Destroy(gameObject);
            Instantiate(blood, transform.position, Quaternion.identity);
            Instantiate(effect, transform.position, Quaternion.identity);
            healthBar.gameObject.SetActive(false);
            Win.LoadScene("WinScene");
        }

        if(Health <= halfHealth)
        {
            anim.SetTrigger("stage2");
        }


        Enemy randomEnemy = enemies[Random.Range(0, enemies.Length)];
        Instantiate(randomEnemy, transform.position + new Vector3(spawnOffset, spawnOffset, 0), transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Player>().takeDmg(damage);
        }
    }
   
}
