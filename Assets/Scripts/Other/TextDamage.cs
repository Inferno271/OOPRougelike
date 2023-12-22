using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDamage : MonoBehaviour
{
    private Vector2 rnd;
    void Start()
    {
        Destroy(gameObject, 1f);
        rnd = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
    }

    void Update()
    {
        transform.Translate(rnd * Time.deltaTime);
    }
}
