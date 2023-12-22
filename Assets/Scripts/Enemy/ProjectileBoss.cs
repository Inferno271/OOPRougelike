using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBoss : MonoBehaviour
{
    [HideInInspector]
    public float damage;
    public GameObject explosion;

    void Start() 
    {
        Destroy(gameObject, 4f);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Wall")
        {
            Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 1f);
            Destroy(gameObject);
        }
        if (other.tag == "Player")
        {
            Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 1f);
            PlayerStats.HP -= damage;
            Destroy(gameObject);
        }
    } 
}
