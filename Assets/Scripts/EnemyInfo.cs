using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 10f;
    [SerializeField]
    private GameObject catPlanet;

    public float MovementSpeed { get; private set; }

    private new Rigidbody rigidbody;
    
    private void Start()
    {
        MovementSpeed = movementSpeed;

        rigidbody = GetComponent<Rigidbody>();
        transform.LookAt(catPlanet.transform.position);
        rigidbody.AddForce(transform.forward * movementSpeed, ForceMode.Force);
    }
}
