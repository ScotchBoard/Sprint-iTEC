using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float bulletSpeed = 10f;
    [SerializeField]
    private GameObject spawnPoint;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        { 
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray: ray, hitInfo: out hit, maxDistance: 100))
            {
                var projectile = Instantiate(bulletPrefab, spawnPoint.transform.position, Quaternion.identity);
                projectile.transform.LookAt(hit.point);
                projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * bulletSpeed, ForceMode.Force);
            }
        }
    }
}
