using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static Character character;
    public Transform spawnHero;
    public static int numberChooseHero;
    public GameObject sword;
    public GameObject archer;
    public GameObject wizard;

    void Awake()
    {
        if (character !=null)
        {
            Destroy(character);
        }
        else
        {
            character = this;
        }
        DontDestroyOnLoad(this);
        switch (numberChooseHero)
        {
            case 1:
                Instantiate(sword, spawnHero.transform.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(archer, spawnHero.transform.position, Quaternion.identity);
                break;
            case 3:
                Instantiate(wizard, spawnHero.transform.position, Quaternion.identity);
                break;
        }
    }
}