using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetInfo : MonoBehaviour
{
    [SerializeField]
    private int healthPoints;

    private int planetHealth;

    private void Awake()
    {
        ResetHealth();
    }

    private void Hurt()
    {
        GameManager.INSTANCE.ResetCombo();
        if (planetHealth > 0)
        {
            planetHealth -= 1;
        }

        if (planetHealth <= 0)
        { 
            GameManager.INSTANCE.GameOver();
        }
    }

    public void ResetHealth()
    {
        planetHealth = healthPoints;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Hurt();
        }
    }
}
