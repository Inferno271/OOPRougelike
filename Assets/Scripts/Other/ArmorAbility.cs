using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorAbility : MonoBehaviour
{
    private Transform player;

    void Start() 
    {   
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); 
        Destroy(gameObject, 6f);
    }

    void Update()
    {
        transform.position = player.transform.position;
    }

    void OnDestroy() 
    {
        PlayerStats.armor = 1f;
    }
}
