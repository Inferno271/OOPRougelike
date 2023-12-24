using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : EnemyBase
{
    [Range(0,5)]
    public float seeDistance;
    [Range(0,5)]
    public float attackDistance;
    private float waitTime, waitTime2;
    public float startWaitTime, startWaitTime2;
    protected bool runToRandom = false;
    protected float rndX, rndY;

    void Update()
    {
        Following();
        CheckDeath();
    }

    protected override void Following()
    {
        if (Vector2.Distance (transform.position, target.transform.position) < seeDistance) 
        {
            anim.SetBool("Running", true);
            runToRandom=false;
            waitTime=1;
            speed = 1.8f;
            FlipX(target.position.x);
            Attack();
        } else {
            anim.SetBool("Running", false);
            speed = 1.2f;
            if (waitTime <= 0)
            {
                rndX= Random.Range(transform.position.x-5f, transform.position.x+5f);
                rndY= Random.Range(transform.position.y-5f, transform.position.y+5f);
                FlipX(rndX);
                runToRandom=true;
                waitTime=startWaitTime;
            } else {
                waitTime -= Time.deltaTime;
            }
        }

        if (runToRandom)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2 (rndX, rndY), speed * Time.deltaTime);
            anim.SetBool("Running", true);
            if (waitTime2 <= 0)
            {
                anim.SetBool("Running", false);
                runToRandom=false;
                waitTime2=startWaitTime2;
            } else {
                waitTime2 -= Time.deltaTime;
            }
        }
    }

    protected override void Attack()
    {
        if (Time.timeScale != 0)
        {
            if (Vector2.Distance (transform.position, target.transform.position) > attackDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            } else {
                PlayerStats.HP -= damage;
            }
        }
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, seeDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
    //Добавить функции
}