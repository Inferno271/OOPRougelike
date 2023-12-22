using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [HideInInspector]
    public int damage;
    private Transform ukaz;

    void Start() 
    {
        ukaz = GameObject.FindGameObjectWithTag("Ukaz").GetComponent<Transform>(); 
        transform.rotation = ukaz.transform.rotation;
        Destroy(gameObject, 0.8f);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyBase>().ShowDamage(damage);
            other.GetComponent<EnemyBase>().TakeDamage(damage);
            Destroy(gameObject);
        }
        if (other.tag == "Wall")
        {
            Destroy(gameObject);
        }
    } 
}
