using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterEnemy : Enemy {

    public float stopDistance;
    private float attackTime;
    public float attackSpeed;


    private void Update()
    {
        if (player != null)
        {
            if(Vector2.Distance(transform.position, player.position) > stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, Speed * Time.deltaTime);
            }
            else
            {
                if(Time.time >= attackTime)
                {
                    attackTime = Time.time + TimeBetweenAttacks;
                    StartCoroutine(Attack());
                }

            }
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
	
