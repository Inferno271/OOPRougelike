using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottleHP : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("Take_HP");
            PlayerStats.numberOfBottle++;
            GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<StartGame>().UpdateBottleNumber();
            Destroy(gameObject);
        }
    }
}
