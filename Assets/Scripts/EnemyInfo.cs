using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    [SerializeField]
    private Vector2 movementSpeedRange;
    [SerializeField]
    private GameObject catPlanet;

    public float MovementSpeed { get; private set; }

    private new Rigidbody rigidbody;
    private EnemySpawn enemySpawn;
    private Quaternion startQuaternion;
    private float movementSpeed;

    private void Start()
    {
        startQuaternion = Quaternion.identity;

        enemySpawn = GameObject.Find("Game Manager").GetComponent<EnemySpawn>();
        MovementSpeed = movementSpeed;

        rigidbody = GetComponent<Rigidbody>();
        LookAtPlanet();
    }

    public void LookAtPlanet()
    {
        transform.LookAt(catPlanet.transform.position);
        movementSpeed = Random.Range(movementSpeedRange.x, movementSpeedRange.y);
        rigidbody.velocity = transform.forward * movementSpeed;
        //rigidbody.AddForce(transform.forward * movementSpeed, ForceMode.Force);
    }

    public void Hurt()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        enemySpawn.Eliminate(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "CatPlanet")
        {
            enemySpawn.Eliminate(this.gameObject);
        }
    }
}
