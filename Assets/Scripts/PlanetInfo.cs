using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetInfo : MonoBehaviour
{
    [SerializeField]
    private int healthPoints;


    private void Hurt()
    {
        if (healthPoints > 0)
        {
            healthPoints -= 1;
        }

        if (healthPoints <= 0)
        { 
            GameManager.INSTANCE.GameOver();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Hurt();
        }
    }
}
