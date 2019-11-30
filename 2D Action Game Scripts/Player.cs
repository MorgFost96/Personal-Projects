using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public float speed;

    private Rigidbody2D rb;
    private Animator anim;
    public int Health;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private Vector2 moveAmount;

    public Animator hurtAnim;

    private SceneTransition Lose;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Lose = GetComponent<SceneTransition>();
    }
    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveAmount = moveInput.normalized * speed;

        if(moveInput != Vector2.zero)
        {
            anim.SetBool("isRun", true);
        }
        else
        {
            anim.SetBool("isRun", false);
        }
    }
    
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);

    }

    //Player Health
    public void takeDmg(int dmgAmt)
    {
        Health -= dmgAmt;
        UpdateHealthUI(Health);
        hurtAnim.SetTrigger("hurt");
        if (Health <= 0)
        {
            Destroy(gameObject);
            Lose.LoadScene("LoseScene");
        }
    }

    public void ChangeWeapon(Weapon weaponToEquip)
    {
        Destroy(GameObject.FindGameObjectWithTag("Weapon"));
        Instantiate(weaponToEquip, transform.position, transform.rotation, transform);
    }

    //The Hearts Length and check's the Arrays of health when they're full and then empty
    void UpdateHealthUI(int currentHealth)
    {
        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    public void Heal(int healAmt)
    {
        if (Health + healAmt > 5)
        {
            Health = 5;
        }
        else
        {
            Health += healAmt;
        }
        UpdateHealthUI(Health);
    }
}
