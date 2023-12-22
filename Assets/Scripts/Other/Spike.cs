using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private bool colWithSpike;

    void Update()
    {
        if (colWithSpike && Time.timeScale == 1) 
            PlayerStats.HP -= 0.08f * PlayerStats.armor;
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Spike")
        {
            colWithSpike = true;
        }
    }

    void OnCollisionExit2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Spike")
        {
            colWithSpike = false;
        }
    }
}
