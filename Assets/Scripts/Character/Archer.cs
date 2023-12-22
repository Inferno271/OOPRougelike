using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : PlayerBase
{
    private GameObject projectile;
    public int damageAbility;
    public float projectileForce;
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    void Update ()
	{
		TakeInput ();
		Move ();
        Attack();
        ArrowAbility();
        TestQ();
    }

    protected override void Attack()
    {
        if (timeBtwAttack <= 0)
        {
            if (Input.GetMouseButton(0) && Time.timeScale == 1)
            { 
                if (!GameObject.FindGameObjectWithTag("GOManager")) return;
                projectile = GameObject.FindGameObjectWithTag("GOManager").GetComponent<GOManager>().bullet[3];
                Vector2 mousePos = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 myPos = transform.position;
                Vector2 direction = (mousePos - myPos).normalized;
                GameObject arrow = Instantiate(projectile, transform.position, Quaternion.identity);
                arrow.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
                arrow.GetComponent<Projectile>().damage = Random.Range(minDamage, maxDamage);
                timeBtwAttack = startTimeBtwAttack;
            }
        } else {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    void ArrowAbility()
    {
        if (timeBtwUseSpell <= 0)
        {
            if (Input.GetKeyDown(KeyCode.F) && Time.timeScale == 1)
            {
                isCooldown = true;
                projectile = GameObject.FindGameObjectWithTag("GOManager").GetComponent<GOManager>().bullet[4];
                Vector2 mousePos = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 myPos = transform.position;
                Vector2 direction = (mousePos - myPos).normalized;
                GameObject arrowAbility = Instantiate(projectile, transform.position, Quaternion.identity);
                arrowAbility.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
                arrowAbility.GetComponent<Projectile>().damage = damageAbility;
                timeBtwUseSpell = startTimeBtwUseSpell;
            }  
        } else {
            timeBtwUseSpell -= Time.deltaTime;
        }

        if (isCooldown)
        {
            StartCooldown(5);
        }
    }
}
