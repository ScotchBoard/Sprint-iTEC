using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovement : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 120.0f;
    [SerializeField]
    private float translationSpeed = 10.0f;
    [SerializeField]
    private float height = 2.0f;             //height from ground level
    [SerializeField]
    private SphereCollider planet;           //collider for planet

    private Transform centre;               //transform for planet
    private float radius;                   //calculated radius from collider

    void Start()
    {
        radius = planet.radius * planet.transform.localScale.y;
        centre = planet.transform;

        // starting position at north pole
        transform.position = centre.position + new Vector3(0, radius + height, 0);
    }

    void Update()
    {
        if (!GameManager.INSTANCE.IsGameOver)
        {
            Translate();
            Align();
            Rotate();
        }
    }

    // Rotation
    private void Rotate()
    {
        float headingDeltaAngle = Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;
        Quaternion headingDelta = Quaternion.AngleAxis(headingDeltaAngle, transform.up);

        transform.rotation = headingDelta * transform.rotation;
    }

    // Movement
    private void Translate()
    {
        float inputMag = Input.GetAxis("Vertical") * translationSpeed * Time.deltaTime;
        transform.position += transform.forward * inputMag;
    }

    // Align the cat to the planet's surface normal
    private void Align()
    {
        Vector3 surfaceNormal = transform.position - centre.position;
        surfaceNormal.Normalize();
        transform.rotation = Quaternion.FromToRotation(transform.up, surfaceNormal) * transform.rotation;
    }
}
