using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHero : MonoBehaviour
{
    private Transform player;
    void Start() 
    {   
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); 
        if (player.GetComponent<Sword>())
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.position = player.transform.position;
        var dir = Input.mousePosition - UnityEngine.Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}