using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetInfo : MonoBehaviour
{
    [SerializeField]
    private int healthPoints;
    [SerializeField]
    private Text planetHealthText;

    private int planetHealth;

    private void Awake()
    {
        ResetHealth();
    }

    private void Start()
    {
        planetHealthText.text = "Purrrlandia's HP: " + planetHealth;
    }

    private void Hurt()
    {
        GameManager.INSTANCE.ResetCombo();
        if (planetHealth > 0)
        {
            planetHealth -= 1;
            planetHealthText.text = "Purrrlandia's HP: " + planetHealth;
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
