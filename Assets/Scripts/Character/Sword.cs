using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : PlayerBase
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public Transform attackPos;
    public LayerMask whatIsEnemies;
    [Range(0,1)]
    public float attackRange;
    public GameObject hit;
    public GameObject armorFX;
    public float energy;

    void Update ()
	{
        Move ();
		TakeInput ();
        Attack();
        Energy();
        Armor();
        TestQ();
    }

    protected override void TakeInput()
    {
        direction = Vector2.zero;
        if (Input.GetKey (KeyCode.W)) {
            attackPos.position = new Vector2 (transform.position.x, transform.position.y+0.3f);
            direction += Vector2.up*0.9f;
        }
        if (Input.GetKey (KeyCode.A)) {
            attackPos.position = new Vector2 (transform.position.x-0.3f, transform.position.y);
            direction += Vector2.left;
        }
        if (Input.GetKey (KeyCode.S)) {
           attackPos.position = new Vector2 (transform.position.x, transform.position.y-0.3f); 
            direction += Vector2.down*0.9f;
        }
        if (Input.GetKey (KeyCode.D)) {
            attackPos.position = new Vector2 (transform.position.x+0.3f, transform.position.y);
            direction += Vector2.right;
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
            attackPos.position = new Vector2 (transform.position.x-0.3f, transform.position.y+0.3f);
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            attackPos.position = new Vector2 (transform.position.x+0.3f, transform.position.y+0.3f);
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
            attackPos.position = new Vector2 (transform.position.x-0.3f, transform.position.y-0.3f);
        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
            attackPos.position = new Vector2 (transform.position.x+0.3f, transform.position.y-0.3f);
        if (NeedDiagonalAdjustment()) direction *= 0.8f;
        AddHP();
    }

    protected override void Attack()
    {
        if (timeBtwAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                damage = Random.Range(minDamage, maxDamage);
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    hitEffect();
                    if (enemiesToDamage[i].tag == "Jar") 
                    {
                        if (enemiesToDamage[i].GetComponent<Jar>())
                            enemiesToDamage[i].GetComponent<Jar>().crash = true;
                    } else {
                        enemiesToDamage[i].GetComponent<EnemyBase>().ShowDamage(damage);
                        enemiesToDamage[i].GetComponent<EnemyBase>().TakeDamage(damage);
                    }
                }
                timeBtwAttack = startTimeBtwAttack;
            }
        } else {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    void Energy()
    {
        if (energy > 10 && Input.GetKey(KeyCode.Space)) {
            energy -= 0.5f;
            Speed = 3.7f;
        } else {
            energy += 0.5f;
            Speed = 2.5f;
        }
        if (energy >= 100) energy = 100;
        if ((energy == 10 && Input.GetKey(KeyCode.Space)) || energy <= 0) energy = 0;
    }

    void Armor()
    {
        if (timeBtwUseSpell <= 0)
        {
            if (Input.GetKeyDown(KeyCode.F) && Time.timeScale == 1)
            {
                isCooldown = true;
                PlayerStats.armor = 0.5f;
                Instantiate(armorFX, transform.position, Quaternion.identity);
                timeBtwUseSpell = startTimeBtwUseSpell;
            }
        } else {
            timeBtwUseSpell -= Time.deltaTime;
        }

        if (isCooldown)
        {
            StartCooldown(10);
        }
    }

    void hitEffect()
    {
        Destroy(Instantiate(hit, attackPos.position, Quaternion.identity), 0.4f);
        FindObjectOfType<AudioManager>().Play("AttackSword1");
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}