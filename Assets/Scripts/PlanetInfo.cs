using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetInfo : MonoBehaviour
{
    [SerializeField]
    private int healthPoints;

    public void Hurt(int damage)
    {
        healthPoints -= damage;
    }
}
