using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public Animator camAnim;

    public void CamShake()
    {
        int rand = Random.Range(0, 5);
        switch(rand)
        {
            case 0:
                break;
            case 1: 
                camAnim.SetTrigger("Shake1");
                break;
            case 2: 
                camAnim.SetTrigger("Shake2");
                break;
            case 3: 
                camAnim.SetTrigger("Shake3");
                break;
            case 4: 
                camAnim.SetTrigger("Shake4");
                break;
            case 5: 
                camAnim.SetTrigger("Shake5");
                break;
        }
            
    }
}
