using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarHP : MonoBehaviour
{
    private Slider sl;

    void Start()
    {
        sl = GetComponent<Slider>();
        sl.maxValue = PlayerStats.HP;
    }

    void Update()
    {
        sl.value = PlayerStats.HP;
    }
}
