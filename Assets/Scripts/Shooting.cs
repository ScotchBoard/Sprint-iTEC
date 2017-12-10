using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    [Header("Weapon")]
    [SerializeField]
    private Text cooling;
    [SerializeField]
    private int coolingTime = 3;

    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float bulletSpeed = 10f;
    [SerializeField]
    private GameObject spawnPoint;
    [SerializeField]
    private int maxBullets = 20;

    private bool isCooled = true;
    private int BulletCount { get; set; }

    private float gunCooldown = 0f;

    void Update()
    {
        //Debug.Log(BulletCount);
        if (Input.GetKeyDown(KeyCode.Mouse0) && isCooled && !GameManager.INSTANCE.IsGamePaused)
        {
            cooling.text = "";
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Debug.DrawLine(transform.position, new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity), Color.red);

            if (Physics.Raycast(ray: ray, hitInfo: out hit, maxDistance: 1000))
            {
                var projectile = Instantiate(bulletPrefab, spawnPoint.transform.position, Quaternion.identity);
                projectile.transform.LookAt(hit.point);

                //Vector3 direction = hit.point - projectile.transform.position;

                projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * bulletSpeed, ForceMode.Force);

                BulletCount++;
                gunCooldown = Time.time + 0.12f; // set the guncooldown to the small wait value
                isCooled = false;

            }
        }

        if (!isCooled && Time.time > gunCooldown)
        {
            isCooled = true;
        }

        if (BulletCount >= maxBullets)
        {
            gunCooldown = Time.time + coolingTime;
            cooling.text = "Weapon is cooling down";
            BulletCount = 0;
        }
    }
}
