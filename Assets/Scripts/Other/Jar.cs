using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jar : MonoBehaviour
{
    public bool crash=false;

    void Update()
    {
        if (crash)
        {
            GetComponent<Animator>().enabled = true;
            FindObjectOfType<AudioManager>().Play("Jar");
            Destroy(this);
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Bullet")
        {
            GetComponent<Animator>().enabled = true;
            FindObjectOfType<AudioManager>().Play("Jar");
            Destroy(other.gameObject);
            Destroy(this);
        }
    }
}
