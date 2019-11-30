using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromancer : Enemy {

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private Vector2 targetPosition;
    private Animator anim;

    public float timeBetweenSummons;
    private float summonTime;

    public Enemy enemyToSummon;

    public float meleeattackspeed;
    public float stopDistance;
    private float timer;
    public float attackSpeed;

    public override void Start()
    {
        base.Start();
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        targetPosition = new Vector2(randomX, randomY);
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //check to see if players dead
        if (player != null)
        {
            if (Vector2.Distance(transform.position, targetPosition) > .5f)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);

                if (Time.time >= summonTime)
                {
                    summonTime = Time.time + timeBetweenSummons;
                    anim.SetTrigger("Summon");
                }
            }

            if (Vector2.Distance(transform.position, player.position) < stopDistance)
            {
                if (Time.time > timer)
                {
                    timer = Time.time + TimeBetweenAttacks;
                    StartCoroutine(Attack());
                }

            }

        }
    }

    //summon's minions
    public void Summon()
    {
        if (player != null)
        {
            Instantiate(enemyToSummon, transform.position, transform.rotation);
        }
    }

    IEnumerator Attack()
    {
        player.GetComponent<Player>().takeDmg(damage);

        Vector2 OriginalPosition = transform.position;
        Vector2 TargetPosition = transform.position;

        float Percent = 0;

        while (Percent <= 1)
        {
            Percent += Time.deltaTime * attackSpeed;
            float formula = (-Mathf.Pow(Percent, 2) + Percent) * 4;
            transform.position = Vector2.Lerp(OriginalPosition, TargetPosition, formula);
            yield return null;
        }

    }
}
