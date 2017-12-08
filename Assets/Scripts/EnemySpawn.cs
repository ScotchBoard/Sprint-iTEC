using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    private int objectsNumber = 10;
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private GameObject catPlanet;
    [SerializeField]
    private float spawnTime = 3f;
    [SerializeField]
    private float distance = 20f;
    [SerializeField]
    private float enemySpeed = 10f;

    void Start()
    {
        Vector3 center = catPlanet.transform.position;
        SphereCollider planetCollider = catPlanet.GetComponent<SphereCollider>();

        for (int i = 0; i < objectsNumber; i++)
        {
            Vector3 spawnPosition = Random.onUnitSphere * (planetCollider.radius + distance) + center;

            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
